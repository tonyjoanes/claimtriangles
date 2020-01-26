namespace TJ.ClaimTriangles.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// CSV Claim Mapper
    /// </summary>
    public static class ClaimCsvMapper
    {
        /// <summary>
        /// Mapper function from string array to input data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<InputData> Map(List<string[]> data)
        {
            var dataList = new List<InputData>();

            if (data == null)
            {
                return dataList;
            }

            for (int i = 0; i < data.Count; i++)
            {
                dataList.Add(new InputData
                {
                    Product = data[i][0],
                    OriginYear = Convert.ToInt32(data[i][1], CultureInfo.InvariantCulture),
                    DevelopmentYear = Convert.ToInt32(data[i][2], CultureInfo.InvariantCulture),
                    Incremental = Convert.ToDouble(data[i][3], CultureInfo.InvariantCulture)
                });
            }

            return dataList;
        }
    }
}
