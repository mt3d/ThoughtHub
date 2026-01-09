namespace ThoughtHub.Services
{
	public interface IContentFactory
	{
		Task<object?> CreateBlockAsync(string typeName);
	}
}
