namespace TJ.ClaimTriangles
{
    using System.IO;

    /// <summary>
    /// Implementation of the FileHelper
    /// </summary>
    public class FileHelper : IFileHelper
    {
        /// <inheritdoc/>
        public bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <inheritdoc/>
        public string[] ReadAllLines(string filePath)
        {
            return File.ReadAllLines(filePath);
        }

        /// <inheritdoc/>
        public StreamWriter CreateText(string filePath)
        {
            return File.CreateText(filePath);
        }
    }
}
