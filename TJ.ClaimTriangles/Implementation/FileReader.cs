using System.IO;

namespace TJ.ClaimTriangles
{
    public class FileReader : IFile
    {
        public bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        public string[] ReadAllLines(string filePath)
        {
            return File.ReadAllLines(filePath);
        }
    }
}
