namespace TJ.ClaimTriangles.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Origin Year Model
    /// </summary>
    public class OriginYear
    {
        public OriginYear()
        {
            DevelopmentYears = new List<DevelopmentYear>();
        }

        public int Year { get; set; }
        public List<DevelopmentYear> DevelopmentYears { get; set; }
    }
}
