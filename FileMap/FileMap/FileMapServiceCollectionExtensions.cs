using FileMap.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace FileMap
{
    public static class FileMapServiceCollectionExtensions
    {
        public static void AddFileMapServices(this IServiceCollection services)
        {
            services.AddSingleton<ILocationFinderFactory, LocationFinderFactory>();
        }
    }
}
