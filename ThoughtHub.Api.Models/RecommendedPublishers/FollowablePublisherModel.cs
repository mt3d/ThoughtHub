using System.Text.Json.Serialization;

namespace ThoughtHub.Api.Models.RecommendedPublishers
{
	[JsonPolymorphic(TypeDiscriminatorPropertyName = "typename")]
	[JsonDerivedType(typeof(UserPublisherModel), "user")]
	[JsonDerivedType(typeof(PublicationPublisherModel), "publication")]
	public class FollowablePublisherModel
	{
		public string Id { get; init; }
	}
}
