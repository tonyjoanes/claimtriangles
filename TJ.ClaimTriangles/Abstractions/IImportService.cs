using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TJ.ClaimTriangles
{
    public interface IImportService
    {
        IEnumerable<InputData> ImportData(string fileLocation, bool containsHeader = true);
    }
}
