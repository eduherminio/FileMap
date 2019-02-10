using System;
using Xunit;

namespace FileMap.Test
{
    public class LocationFinderFactoryTest
    {
        private const string _inputFile = "TestFixtures/input.txt";
        private const string _locationsFile = "TestFixtures/locs.txt";

        private string OutputFile() => $"TestFixtures/output_{Guid.NewGuid().ToString()}.out";

        [Fact]
        public void GetLocationFinder()
        {
            string outputFile = OutputFile();

            ILocationFinder locationFinder = LocationFinderFactory.GetLocationFinder(_inputFile, outputFile, _locationsFile);

            Assert.Equal(locationFinder, LocationFinderFactory.GetLocationFinder(_inputFile, outputFile, _locationsFile));
        }

        [Fact]
        public void ShouldNotGetLocationFinderProvidingNonExistingInputFile()
        {
            Assert.Throws<ArgumentException>(() => LocationFinderFactory.GetLocationFinder($"C:/{Guid.NewGuid().ToString()}", OutputFile(), _locationsFile));
        }

        [Fact]
        public void ShouldNotGetLocationFinderProvidingNonExistingLocalizationsFile()
        {
            Assert.Throws<ArgumentException>(() => LocationFinderFactory.GetLocationFinder(_inputFile, OutputFile(), $"C:/{Guid.NewGuid().ToString()}"));
        }

        [Fact]
        public void ShouldNotGetLocationFinderProvidingNonExistingOutputFileParentFolder()
        {
            Assert.Throws<ArgumentException>(() => LocationFinderFactory.GetLocationFinder(_inputFile, "NonExistingFolder/output.out", _locationsFile));
        }
    }
}
