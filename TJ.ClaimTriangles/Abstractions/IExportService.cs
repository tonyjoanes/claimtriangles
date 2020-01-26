namespace TJ.ClaimTriangles
{
    using TJ.ClaimTriangles.Models;

    /// <summary>
    /// Export service interface
    /// </summary>
    public interface IExportService
    {
        /// <summary>
        /// Export output model to the specified path
        /// </summary>
        /// <param name="outputModel"></param>
        /// <param name="outputPath"></param>
        void Export(OutputModel outputModel, string outputPath);
    }
}
