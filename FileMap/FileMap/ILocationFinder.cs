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
        /// <param name="inputFilePath"></param>
        /// <param name="outputFilePath"></param>
        /// <param name="locationsFilePath"></param>
        /// <returns></returns>
        Task MapLocations(string inputFilePath, string outputFilePath, string locationsFilePath);
    }
}
