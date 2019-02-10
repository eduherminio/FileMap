using FileParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMap.Impl
{
    internal class LocationFinder : ILocationFinder
    {
        private const char _separator = '\u0009';

        private readonly string _inputFilePath;
        private readonly string _outputFilePath;
        private readonly string _locationsFilePath;

        internal LocationFinder(string inputFilePath, string outputFilePath, string locationsFilePath)
        {
            _inputFilePath = inputFilePath;
            _outputFilePath = outputFilePath;
            _locationsFilePath = locationsFilePath;
        }

        public async Task MapLocations()
        {
            IEnumerable<string> inputList = ReadInputs();

            Dictionary<string, string> inputRawLineDictionary = inputList.ToDictionary((input) => input);

            using (FileStream fs = File.Open(_locationsFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader locationsStreamReader = new StreamReader(bs))
            {
                string line;
                while ((line = locationsStreamReader.ReadLine()) != null)
                {
                    string code = ExtractLocationCode(line);
                    if (inputList.Contains(code))
                    {
                        inputRawLineDictionary[code] = line;
                    }
                }
            }

            using (StreamWriter outputStreamWriter = new StreamWriter(_outputFilePath))
            {
                foreach (var pair in inputRawLineDictionary)
                {
                    outputStreamWriter.WriteLine(ExtractLocationName(pair.Value));
                }
            }
        }

        private IEnumerable<string> ReadInputs()
        {
            IParsedFile inputFile = new ParsedFile(_inputFilePath);
            while (!inputFile.Empty)
            {
                yield return inputFile.NextLine().ToSingleString();
            }
        }

        private static string ExtractLocationCode(string parsedLine)
        {
            return parsedLine.Split(_separator).First();
        }

        private static string ExtractLocationName(string parsedLine)
        {
            var tabStartIndex = parsedLine.IndexOf(_separator);

            return parsedLine
                .Substring(tabStartIndex)
                .TrimStart();
        }
    }
}
