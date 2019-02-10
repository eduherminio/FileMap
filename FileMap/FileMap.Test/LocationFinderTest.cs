using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace FileMap.Test
{
    public class LocationFinderTest
    {
        private const string _inputFile = "TestFixtures/input.txt";
        private const string _expectedOutputFile = "TestFixtures/output.txt";
        private const string _locationsFile = "TestFixtures/locs.txt";

        private string OutputFile() => $"TestFixtures/output_{Guid.NewGuid().ToString()}.out";

        [Fact]
        public async Task MapLocations()
        {
            string outputFile = OutputFile();
            ILocationFinder finder = LocationFinderFactory.GetLocationFinder(_inputFile, outputFile, _locationsFile);

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
            ILocationFinder finder = LocationFinderFactory.GetLocationFinder(_inputFile, initialOutputFile, _locationsFile);

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
