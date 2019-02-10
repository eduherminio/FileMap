using FileMap.Impl;
using System;
using System.Collections.Concurrent;
using System.IO;

namespace FileMap
{
    public static class LocationFinderFactory
    {
        private static readonly ConcurrentDictionary<int, ILocationFinder> _locationFinders = new ConcurrentDictionary<int, ILocationFinder>();

        /// <summary>
        /// Gets an ILocationFinder instance
        /// </summary>
        /// <param name="inputFilePath">Input file path</param>
        /// <param name="outputFilePath">Output file path (will be overwritten)</param>
        /// <param name="locationsFilePath">Locations file path</param>
        /// <returns></returns>
        public static ILocationFinder GetLocationFinder(string inputFilePath, string outputFilePath, string locationsFilePath)
        {
            ValidateFilePaths(inputFilePath, outputFilePath, locationsFilePath);

            int hashCode = CalculateStringListHashCode(inputFilePath, outputFilePath, locationsFilePath);

            _locationFinders.GetOrAdd(hashCode, new LocationFinder(inputFilePath, outputFilePath, locationsFilePath));

            return _locationFinders[hashCode];
        }

        private static int CalculateStringListHashCode(params string[] strings)
        {
            const int prime = 31;
            int result = 1;

            foreach (string str in strings)
            {
                result = result * prime + str.GetHashCode();
            }

            return result;
        }

        private static void ValidateFilePaths(string inputFilePath, string outputFilePath, string locationsFilePath)
        {
            ValidateFileExistence(inputFilePath);
            ValidateFileExistence(locationsFilePath);
            ValidateDirectoryExistence(Path.GetDirectoryName(outputFilePath));

            if (File.Exists(outputFilePath))
            {
                ConsoleColor originalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;

                Console.WriteLine($"[WARNING] {outputFilePath} already exists, it will be overwritten");

                Console.ForegroundColor = originalColor;
            }
        }

        private static void ValidateFileExistence(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException(
                    $"File {filePath} doesn't exist");
            }
        }

        private static void ValidateDirectoryExistence(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new ArgumentException(
                    $"Directory {directoryPath} doesn't exist");
            }
        }
    }
}
