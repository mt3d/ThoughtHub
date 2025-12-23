using Microsoft.EntityFrameworkCore;

namespace ThoughtHub.Data.Entities.Media
{
	public class ImageFolder
	{
		public int Id { get; set; }

		/** A self-referencing relationship. Will be modeled as a foreign key
		 *  constraint from [ParentId] to [Id] in the same table.
		 *  
		 *  In this relationship, the ON DELETE CASCADE behavior is added automatically
		 *  by EF Core because it assumes that deleting a parent should also delete its children.
		 *  
		 *  This is the code generated in the migrations:
		 *  constraints: table =>
		 *  {
		 *		table.PrimaryKey("PK_ImageFolders", x => x.Id);
		 *		table.ForeignKey(
         *      name: "FK_ImageFolders_ImageFolders_ParentId",
         *      column: x => x.ParentId,
         *      principalTable: "ImageFolders",
         *      principalColumn: "Id",
         *      onDelete: ReferentialAction.Cascade);
         *  });
         *  
         *  However, SQL Server does not allow multiple cascading paths that can delete the
         *  same row, since that could cause ambiguity or cycles.
         *  
         *  For example, if Image also has a cascade delete rule on its ImageFolderId,
         *  then deleting a folder could cascade to both its subfolders and its images,
         *  and SQL Server would see that as multiple paths to the same deletion.
         *  
         *  public class Image
         *  {
         *		public ImageFolder Folder { get; set; }
         *	}
         *  
         *  Deleting a folder automatically deletes its images in the database.
         *  Deleting a folder automatically deletes its subfolders => each subfolder deletes its images.
         *  
         *  Now we have two cascading paths that start from the same table:
         *  Path 1: ImageFolder -> ImageFolder (delete parent → delete subfolder) -> Image
         *  Path 2: ImageFolder -> Image (delete folder → delete image)
         *  
         *  When we delete a parent folder, the same “delete operation” can cascade to
         *  Images through two different routes: one directly from the parent, and another
         *  indirectly through its subfolders.
         *  
         *  SQL Server does not allow this situation because it can cause ambiguity
         *  or recursive deletion loops that are hard to resolve at the database level.
		 */

		public int ParentFolderId { get; set; }

		public ImageFolder? Parent { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public DateTime CreatedAt { get; set; }

		public IList<Image> Images { get; set; } = new List<Image>();

        public static void OnModelCreating(ModelBuilder builder)
        {
			builder.Entity<ImageFolder>()
	            .HasOne(f => f.Parent)
	            .WithMany() // Each folder can be a parent of many folders.
	            .HasForeignKey(f => f.ParentFolderId)
	            .OnDelete(DeleteBehavior.NoAction);
		}
	}
}
