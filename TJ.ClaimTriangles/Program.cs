namespace TJ.ClaimTriangles
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;
    using TJ.ClaimTriangles.Implementation;

    class Program
    {
        static void Main(string[] args)
        {
            var configuration = GetConfiguration();

            var outputDirectory = configuration.GetSection("OutputDirectory").Value;
            var inputDirectory = configuration.GetSection("InputDirectory").Value;

            var claimDataHandler = new ClaimDataHandler(
                new CSVImportService(new FileHelper(), ClaimCsvMapper.Map),
                new CSVExporter(new FileHelper()));

            claimDataHandler.Invoke(inputDirectory, outputDirectory);

            Console.WriteLine("Calculations Completed");
            Console.ReadLine();
        }

        /// <summary>
        /// Gets a Configuration object.
        /// </summary>
        /// <returns>IConfiguration</returns>
        static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            return configuration;
        }
    }
}
