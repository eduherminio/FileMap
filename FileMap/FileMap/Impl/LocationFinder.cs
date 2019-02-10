using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileMap.Impl
{
    internal class LocationFinder : ILocationFinder
    {
        private readonly string _inputFilePath;
        private readonly string _outputFilePath;
        private readonly string _locationsFilePath;

        internal LocationFinder(string inputFilePath, string outputFilePath, string locationsFilePath)
        {
            _inputFilePath = inputFilePath;
            _outputFilePath = outputFilePath;
            _locationsFilePath = locationsFilePath;
        }

        public bool MapLocations()
        {
            throw new NotImplementedException();
        }
    }
}
