using System;

namespace FileMap.ConsoleClient
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 3)
            {
                ILocationFinder locationFinder = LocationFinderFactory.GetLocationFinder(args[0], args[1], args[2]);
                locationFinder.MapLocations();
            }
            else
            {
                PrintHelp();
            }
        }

        private static void PrintHelp()
        {
            Console.WriteLine(@"
Usage three arguments are required:
* Input file path
* Output file path
* Locations file path
\nExample: dotnet run C:/inputFile.txt D:/outputFile.txt E:/locationsFile.csv
");
        }
    }
}
