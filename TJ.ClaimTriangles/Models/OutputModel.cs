using System;
using System.Collections.Generic;
using System.Text;

namespace TJ.ClaimTriangles.Models
{
    public class OutputModel
    {
        public int EarliestYear { get; set; }
        public int NumberOfDevelopmentYears { get; set; }
        public List<Product> Products { get; set; }
    }
}
