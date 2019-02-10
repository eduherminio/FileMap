using FileMap.Impl;
using System;
using Xunit;

namespace FileMap.Test
{
    public class LocationFinderFactoryTest : BaseFileMapTest
    {
        private readonly ILocationFinderFactory _locationFinderFactory;

        public LocationFinderFactoryTest()
        {
            _locationFinderFactory = new LocationFinderFactory();
        }

        [Fact]
        public void GetLocationFinder()
        {
            string outputFile = OutputFile();

            ILocationFinder locationFinder = _locationFinderFactory.GetLocationFinder(_inputFile, outputFile, _locationsFile);

            Assert.Equal(locationFinder, _locationFinderFactory.GetLocationFinder(_inputFile, outputFile, _locationsFile));
        }

        [Fact]
        public void ShouldNotGetLocationFinderProvidingNonExistingInputFile()
        {
            Assert.Throws<ArgumentException>(() => _locationFinderFactory.GetLocationFinder($"C:/{Guid.NewGuid().ToString()}", OutputFile(), _locationsFile));
        }

        [Fact]
        public void ShouldNotGetLocationFinderProvidingNonExistingLocalizationsFile()
        {
            Assert.Throws<ArgumentException>(() => _locationFinderFactory.GetLocationFinder(_inputFile, OutputFile(), $"C:/{Guid.NewGuid().ToString()}"));
        }

        [Fact]
        public void ShouldNotGetLocationFinderProvidingNonExistingOutputFileParentFolder()
        {
            Assert.Throws<ArgumentException>(() => _locationFinderFactory.GetLocationFinder(_inputFile, "NonExistingFolder/output.out", _locationsFile));
        }
    }
}
