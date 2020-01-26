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

            // import data and map to input data model
            var importer = new CSVImportService(new FileHelper(), ClaimCsvMapper.Map);
            var data = importer.ImportData(inputDirectory);
            
            var output = new ClaimDataHandler().GetCalculatedProducts(data);

            var exporter = new CSVExporter(new FileHelper());

            exporter.Export(output, outputDirectory);


            Console.WriteLine($"Hello World! {configuration.GetConnectionString("Storage")}");
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
