using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TJ.ClaimTriangles.Implementation;
using TJ.ClaimTriangles.Models;

namespace TJ.ClaimTriangles
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = GetConfiguration();

            var outputDirectory = configuration.GetSection("OutputDirectory").Value;


            // import data and map to input data model
            var importer = new CSVImportService(new FileReader(), ClaimCsvMapper.Map);
            var data = importer.ImportData(@"C:\dev\claimtriangles\input-data.csv");


            var originYearVsDevYear = GetOriginYears(data);
            var products = GetProducts(data);

            // for each product iterate through origin year and accumulate development year data

            var currentYear = originYearVsDevYear.First().Year;

            foreach (var oYear in originYearVsDevYear)
            {
                Console.WriteLine($"{oYear.Year}");

                foreach (var developmentYears in oYear.DevelopmentYears)
                {
                    foreach (var product in products)
                    {
                        var dataPoint = data.FirstOrDefault(x => x.Product == product.Name
                            && x.OriginYear == oYear.Year
                            && x.DevelopmentYear == developmentYears.Year);

                        var index = product.Values.Count;
                        var previousValue = index == 0
                            ? 0
                            : product.Values[index - 1];


                        // we dont want an incremental if were on a new origin year
                        // need a nicer way for this!!
                        double incrementalValue = 0;
                        if (currentYear == oYear.Year)
                        {
                            incrementalValue = dataPoint != null
                              ? dataPoint.Incremental
                              : 0;
                        }
                        else
                        {
                            currentYear = oYear.Year;
                            incrementalValue = dataPoint == null ? 0 : dataPoint.Incremental;
                            previousValue = 0;
                        }

                        product.Values.Add(previousValue + incrementalValue);
                    }
                }


            }


            var output = new OutputModel
            {
                EarliestYear = originYearVsDevYear.First().Year,
                NumberOfDevelopmentYears = originYearVsDevYear.Count,
                Products = products
            };



            Console.WriteLine($"Hello World! {configuration.GetConnectionString("Storage")}");
            Console.ReadLine();
        }

        static List<Product> GetProducts(IEnumerable<InputData> data)
        {
            return data
                .GroupBy(x => x.Product)
                .Select(x => new Product
                {
                    Name = x.Key
                })
                .ToList();
        }

        static List<OriginYear> GetOriginYears(IEnumerable<InputData> data)
        {
            // process the origin year vs development year tables
            var originYears = new List<OriginYear>();

            var orderedOriginYears = data
                .OrderBy(x => x.OriginYear)
                .Select(x => x.OriginYear);

            foreach (var item in orderedOriginYears)
            {
                // do we have an origin year setup with this year?
                var originYear = originYears.FirstOrDefault(x => x.Year == item);

                if (originYear == null)
                {
                    var newOriginYear = new OriginYear { Year = item };

                    var developmentYears = data
                        .Where(x => x.DevelopmentYear >= item)
                        .OrderBy(x => x.DevelopmentYear)
                        .Select(x => x.DevelopmentYear)
                        .Distinct()
                        .Select(x => new DevelopmentYear
                        {
                            Year = x,
                            OriginYear = newOriginYear
                        })
                        .ToList();

                    originYears.Add(new OriginYear
                    {
                        Year = item,
                        DevelopmentYears = developmentYears
                    });
                }
            }

            return originYears;
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
