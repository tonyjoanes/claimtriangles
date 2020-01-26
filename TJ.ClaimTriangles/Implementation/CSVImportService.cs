namespace TJ.ClaimTriangles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CSVImportService : IImportService
    {
        private readonly IFileHelper fileReader;
        private readonly Func<List<string[]>, List<InputData>> mapClaimData;

        public CSVImportService(IFileHelper fileReader, Func<List<string[]>, List<InputData>> mapClaimData)
        {
            this.fileReader = fileReader;
            this.mapClaimData = mapClaimData;
        }

        public IEnumerable<InputData> ImportData(string fileLocation, bool containsHeader = true)
        {
            if (!fileReader.Exists(fileLocation))
            {
                return new List<InputData>();
            }

            var lines = fileReader
                .ReadAllLines(fileLocation)
                .Select(a => a.Split(','));

            var skipNumber = containsHeader
                ? 1 
                : 0;

            var csv = (from line in lines
                       select line)
                       .Skip(skipNumber)
                       .ToList();

            return mapClaimData(csv);
        }
    }
}
