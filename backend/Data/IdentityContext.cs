using backend.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
	public class IdentityContext : IdentityDbContext<User>
	{
		public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }
	}
}
