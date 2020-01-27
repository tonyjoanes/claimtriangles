namespace TJ.ClaimTriangles.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Product Model
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Initialise an instance of a Product
        /// </summary>
        public Product()
        {
            Values = new List<double>();
        }

        public string Name { get; set; }
        public List<double> Values { get; set; }
    }
}
