using System;
using System.Collections.Generic;
using System.Text;

namespace TJ.ClaimTriangles.Models
{
    public class OriginYear
    {
        public int Year { get; set; }
        public List<DevelopmentYear> DevelopmentYears { get; set; }
    }
}
