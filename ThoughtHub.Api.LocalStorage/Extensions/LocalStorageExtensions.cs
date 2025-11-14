using Microsoft.Extensions.DependencyInjection;
using ThoughtHub.Storage;

namespace ThoughtHub.Api.LocalStorage.Extensions
{
	public static class LocalStorageExtensions
	{
		public static IServiceCollection AddLocalFileStorage(
			this IServiceCollection services,
			string? baseUrl = null)
		{
			services.Add(new ServiceDescriptor(typeof(IStorage), sp => new LocalStorage(baseUrl), ServiceLifetime.Scoped));

			return services;
		}
	}
}
