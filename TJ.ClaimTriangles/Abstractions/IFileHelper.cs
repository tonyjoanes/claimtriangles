namespace TJ.ClaimTriangles
{
    using System.IO;

    /// <summary>
    /// FileHelper class to wrap the System.IO Implementation.
    /// </summary>
    public interface IFileHelper
    {
        /// <summary>
        /// Open text file and read all lines
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>string[]</returns>
        string[] ReadAllLines(string filePath);

        /// <summary>
        /// Checks for file existence
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Boolean</returns>
        bool Exists(string filePath);

        /// <summary>
        /// Create a text stream writer from file path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>StreamWriter</returns>
        StreamWriter CreateText(string filePath);
    }
}
