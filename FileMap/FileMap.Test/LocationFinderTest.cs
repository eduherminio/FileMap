using FileMap.Impl;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace FileMap.Test
{
    public class LocationFinderTest : BaseFileMapTest
    {
        private const string _expectedOutputFile = "TestFixtures/output.txt";

        private readonly ILocationFinderFactory _locationFinderFactory;

        public LocationFinderTest()
        {
            _locationFinderFactory = new LocationFinderFactory();
        }

        [Fact]
        public async Task MapLocations()
        {
            string outputFile = OutputFile();
            ILocationFinder finder = _locationFinderFactory.GetLocationFinder(_inputFile, outputFile, _locationsFile);

            await finder.MapLocations();

            using (StreamReader expectedFileStreamReader = new StreamReader(_expectedOutputFile))
            using (StreamReader outputFileStreamReader = new StreamReader(outputFile))
            {
                string expectedLine;
                while ((expectedLine = expectedFileStreamReader.ReadLine()) != null)
                {
                    Assert.Equal(expectedLine, outputFileStreamReader.ReadLine());
                }

                Assert.Null(outputFileStreamReader.ReadLine());
            }
        }

        [Fact]
        public async Task MapLocationsOverridingFilePaths()
        {
            string initialOutputFile = OutputFile();
            ILocationFinder finder = _locationFinderFactory.GetLocationFinder(_inputFile, initialOutputFile, _locationsFile);

            string finalOutputFile = OutputFile();
            await finder.MapLocations(_inputFile, finalOutputFile, _locationsFile);

            using (StreamReader expectedFileStreamReader = new StreamReader(_expectedOutputFile))
            using (StreamReader outputFileStreamReader = new StreamReader(finalOutputFile))
            {
                string expectedLine;
                while ((expectedLine = expectedFileStreamReader.ReadLine()) != null)
                {
                    Assert.Equal(expectedLine, outputFileStreamReader.ReadLine());
                }

                Assert.Null(outputFileStreamReader.ReadLine());
            }

            Assert.NotEqual(initialOutputFile, finalOutputFile);
            Assert.False(File.Exists(initialOutputFile));
        }
    }
}
