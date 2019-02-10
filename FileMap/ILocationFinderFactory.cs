namespace FileMap
{
    /// <summary>
    /// Handles ILocationFinder instances
    /// </summary>
    public interface ILocationFinderFactory
    {
        /// <summary>
        /// Gets an ILocationFinder instance
        /// </summary>
        /// <param name="inputFilePath">Input file path</param>
        /// <param name="outputFilePath">Output file path (will be overwritten)</param>
        /// <param name="locationsFilePath">Locations file path</param>
        /// <returns></returns>
        ILocationFinder GetLocationFinder(string inputFilePath, string outputFilePath, string locationsFilePath);
    }
}
