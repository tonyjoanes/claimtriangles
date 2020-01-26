namespace TJ.ClaimTriangles.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Output Model
    /// </summary>
    public class OutputModel
    {
        public int EarliestYear { get; set; }
        public int NumberOfDevelopmentYears { get; set; }
        public List<Product> Products { get; set; }

        public string OutputPath { get; set; }
    }
}
