using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace FileMap.Test
{
    public class FileMapServiceCollectionExtensionsTest : BaseFileMapTest
    {
        [Fact]
        public void AddFileMapServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddFileMapServices();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            string outputFile = OutputFile();
            ILocationFinder finder1, finder2;

            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                ILocationFinderFactory singletonFactory = scope.ServiceProvider.GetRequiredService<ILocationFinderFactory>();
                finder1 = singletonFactory.GetLocationFinder(_inputFile, outputFile, _locationsFile);
            }

            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                ILocationFinderFactory singletonFactory = scope.ServiceProvider.GetRequiredService<ILocationFinderFactory>();
                finder2 = singletonFactory.GetLocationFinder(_inputFile, outputFile, _locationsFile);
            }

            Assert.Equal(finder1, finder2);
        }
    }
}
