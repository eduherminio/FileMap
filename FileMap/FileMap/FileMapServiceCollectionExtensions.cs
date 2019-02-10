using Microsoft.Extensions.DependencyInjection;

namespace FileMap
{
    public static class FileMapServiceCollectionExtensions
    {
        private static readonly LocationFinderFactory _locationFinderFactory = new LocationFinderFactory();

        public static void AddFileMapServices(this IServiceCollection services)
        {
            services.AddSingleton(_locationFinderFactory);
        }
    }
}
