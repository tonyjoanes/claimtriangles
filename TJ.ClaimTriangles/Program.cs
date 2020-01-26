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

            foreach (var oYear in originYearVsDevYear)
            {
                Console.WriteLine($"{oYear.Year}");


                var currentYear = data.Where(x => x.OriginYear == oYear.Year);

                foreach (var developmentYears in currentYear)
                {
                    foreach (var product in products)
                    {
                        var dataPoint = data.FirstOrDefault(x => x.Product == product.Name
                            && x.OriginYear == developmentYears.OriginYear
                            && x.DevelopmentYear == developmentYears.DevelopmentYear);

                        if (dataPoint != null)
                        {
                            var index = product.Values.Count;
                            var previousValue = product.Values.Count == 0 
                                ? 0 
                                : product.Values[index - 1];
                            product.Values.Add(previousValue + dataPoint.Incremental);
                        }
                        else
                        {
                            product.Values.Add(0);
                        }
                    }
                }

                
            }
            //foreach(var product in products)
            //{
            //    Console.WriteLine(product.Name);
                
            //    foreach (var originVsDevelopment in originYearVsDevYear)
            //    {
            //        // do we have data for this origin year?
            //        var oy = data.Where(x => x.Product == product.Name
            //        && x.OriginYear == originVsDevelopment.Year).ToList();

            //        if (oy.Count > 0)
            //        {
            //            var index = product.Values.Count;
            //            var previousValue = product.Values[index - 1];
            //            product.Values.Add(previousValue + oy[0].Incremental);
            //        }
            //        else
            //        {
            //            product.Values.Add(0);
            //        }
                    
            //    }
            //}


            // export the data





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
                        .OrderBy(x => x.DevelopmentYear)
                        .Select(x => x.DevelopmentYear)
                        .Distinct()
                        .Select(x => new DevelopmentYear
                        {
                            Year = item,
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
