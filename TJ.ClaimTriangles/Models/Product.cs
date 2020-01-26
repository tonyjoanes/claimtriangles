using System;
using System.Collections.Generic;
using System.Text;

namespace TJ.ClaimTriangles.Models
{
    public class Product
    {
        public Product()
        {
            Values = new List<double>();
        }

        public string Name { get; set; }
        public List<double> Values { get; set; }
    }
}
