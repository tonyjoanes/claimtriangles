namespace TJ.ClaimTriangles
{
    public interface IFile
    {
        string[] ReadAllLines(string filePath);
        bool Exists(string filePath);
    }
}
