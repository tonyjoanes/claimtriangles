namespace TJ.ClaimTriangles
{
    using System.Collections.Generic;

    /// <summary>
    /// Import Service Interface
    /// </summary>
    public interface IImportService
    {
        /// <summary>
        /// Imports the data from specified file location into InputData list
        /// </summary>
        /// <param name="fileLocation"></param>
        /// <param name="containsHeader"></param>
        /// <returns></returns>
        IEnumerable<InputData> ImportData(string fileLocation, bool containsHeader = true);
    }
}
