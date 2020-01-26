namespace TJ.ClaimTriangles.Implementation
{
    using System;
    using System.IO;
    using TJ.ClaimTriangles.Models;

    /// <summary>
    /// CSV Implementation of the Exporter Service Interface
    /// </summary>
    public class CSVExporter : IExportService
    {
        private readonly IFileHelper fileHelper;

        /// <summary>
        /// Initialises an instance of the CSVExporter.
        /// </summary>
        /// <param name="fileHelper"></param>
        public CSVExporter(IFileHelper fileHelper)
        {
            this.fileHelper = fileHelper;
        }

        /// <summary>
        /// Export the output model to a CSV file
        /// </summary>
        /// <param name="outputModel"></param>
        /// <param name="outputPath"></param>
        public void Export(OutputModel outputModel, string outputPath)
        {
            if (outputModel == null)
            {
                throw new ArgumentNullException(nameof(outputModel));
            }

            using (var streamWriter = File.CreateText(outputPath))
            {
                streamWriter.WriteLine($"{outputModel.EarliestYear}, {outputModel.NumberOfDevelopmentYears}");

                foreach (var product in outputModel.Products)
                {
                    var valuesAsString = String.Join(',', product.Values);
                    streamWriter.WriteLine($"{product.Name}, {valuesAsString}");
                }
            }
        }
    }
}
