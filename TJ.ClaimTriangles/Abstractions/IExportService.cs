using System;
using System.Collections.Generic;
using System.Text;
using TJ.ClaimTriangles.Models;

namespace TJ.ClaimTriangles
{
    public interface IExportService
    {
        void Export(OutputModel outputModel, string outputPath);
    }
}
