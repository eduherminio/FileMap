using System.Threading.Tasks;

namespace FileMap
{
    public interface ILocationFinder
    {
        /// <summary>
        /// Perform locations mapping
        /// </summary>
        /// <returns></returns>
        Task MapLocations();

        /// <summary>
        /// Perform locations mapping, overriding preconfigured file paths
        /// </summary>
        /// <param name="inputFilePath">New input file path</param>
        /// <param name="outputFilePath">New output file path (will be overwritten)</param>
        /// <param name="locationsFilePath">New locations file path</param>
        /// <returns></returns>
        Task MapLocations(string inputFilePath, string outputFilePath, string locationsFilePath);
    }
}
