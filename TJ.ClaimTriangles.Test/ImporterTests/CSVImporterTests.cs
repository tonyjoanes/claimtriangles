using Moq;
using System.Collections.Generic;
using Xunit;

namespace TJ.ClaimTriangles.Test.ImporterTests
{
    public class CSVImporterTests
    {
        [Fact]
        public void ImportData_EmptyFilePath_ReturnsEmptyDataList()
        {
            var sut = new CSVImportService(new FileHelper(), x => new List<InputData>());

            var actual = sut.ImportData("");

            Assert.Empty(actual);
        }

        [Fact]
        public void ImportData_WithFilePath_ReadsFile()
        {
            var mockFileReader = new Mock<IFileHelper>();
            mockFileReader
                .Setup(r => r.ReadAllLines(It.IsAny<string>()))
                .Returns(new string[]
                {
                    "test",
                    "1990"
                });

            mockFileReader.Setup(r => r.Exists(It.IsAny<string>())).Returns(true);

            var sut = new CSVImportService(mockFileReader.Object, x => new List<InputData>());

            var actual = sut.ImportData(@"C:\temp\test.csv");

            mockFileReader.Verify(r => r.ReadAllLines(It.IsAny<string>()), Times.Once);
        }
    }
}
