using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FileMap.ConsoleClient
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Length == 3)
            {
                await MapLocations(args[0], args[1], args[2]);
            }
            else
            {
                PrintHelp();
            }

            Console.ReadKey();
        }

        private static async Task MapLocations(string inputFile, string outputFile, string locationsFile)
        {
            Stopwatch watch = Stopwatch.StartNew();
            ILocationFinder locationFinder = LocationFinderFactory.GetLocationFinder(inputFile, outputFile, locationsFile);

            await locationFinder.MapLocations().ContinueWith((task) =>
            {
                watch.Stop();
                ConsoleColor originalColor = Console.ForegroundColor;
                if (task.IsCompletedSuccessfully)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Mapping from {inputFile} to {outputFile} using {locationsFile} was successfully completed");
                }
                else
                {
                    Console.WriteLine($"Mapping from {inputFile} to {outputFile} using {locationsFile} was completed with errors:");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(task.Exception.Message);
                }
                Console.ForegroundColor = originalColor;
                Console.WriteLine($"{watch.ElapsedMilliseconds}ms elapsed");
            });
        }

        private static void PrintHelp()
        {
            Console.WriteLine(@"
Usage three arguments are required:
* Input file path
* Output file path
* Locations file path

Example:    dotnet run C:/inputFile.txt D:/outputFile.txt E:/locationsFile.csv
");
        }
    }
}
