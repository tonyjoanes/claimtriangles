namespace TJ.ClaimTriangles
{
    using System.Collections.Generic;
    using System.Linq;
    using TJ.ClaimTriangles.Models;

    /// <summary>
    /// Handles the creation of accumulated claim data
    /// </summary>
    public class ClaimDataHandler : IClaimProcessor
    {
        /// <summary>
        /// Calculate and build Product list with accumulated data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public OutputModel GetCalculatedProducts(IEnumerable<InputData> data)
        {
            var originYearVsDevYear = GetOriginYears(data);
            var products = GetProducts(data);
            var currentYear = originYearVsDevYear.First().Year;

            foreach (var oYear in originYearVsDevYear)
            {
                foreach (var developmentYears in oYear.DevelopmentYears)
                {
                    foreach (var product in products.Where(x => x.Name == "Non-Comp"))
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

            return output;
        }

        /// <summary>
        /// Get a list of products
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private List<Product> GetProducts(IEnumerable<InputData> data)
        {
            return data
                .GroupBy(x => x.Product)
                .Select(x => new Product
                {
                    Name = x.Key
                })
                .ToList();
        }

        /// <summary>
        /// Build up a list of origin years
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private List<OriginYear> GetOriginYears(IEnumerable<InputData> data)
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
    }
}
