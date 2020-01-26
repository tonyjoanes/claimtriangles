using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TJ.ClaimTriangles.Test.AccumulatorTests
{
    public partial class Class1
    {
        [Fact]
        public void CanRunATest()
        {
            //var exampleList = new List<InputData>
            //{
            //    new InputData { Product = "Comp", OriginYear = 1992, DevelopmentYear = 1992, Incremental = 110.0},
            //    new InputData { Product = "Comp", OriginYear = 1992, DevelopmentYear = 1993, Incremental = 170.0},
            //    new InputData { Product = "Comp", OriginYear = 1993, DevelopmentYear = 1993, Incremental = 200.0},
            //    new InputData { Product = "Non-Comp", OriginYear = 1990, DevelopmentYear = 1990, Incremental = 45.2},
            //    new InputData { Product = "Non-Comp", OriginYear = 1990, DevelopmentYear = 1991, Incremental = 64.8},
            //    new InputData { Product = "Non-Comp", OriginYear = 1990, DevelopmentYear = 1993, Incremental = 37.0},
            //    new InputData { Product = "Non-Comp", OriginYear = 1991, DevelopmentYear = 1991, Incremental = 50.0},
            //    new InputData { Product = "Non-Comp", OriginYear = 1991, DevelopmentYear = 1992, Incremental = 75.0},
            //    new InputData { Product = "Non-Comp", OriginYear = 1991, DevelopmentYear = 1993, Incremental = 25.0},
            //    new InputData { Product = "Non-Comp", OriginYear = 1992, DevelopmentYear = 1992, Incremental = 55.0},
            //    new InputData { Product = "Non-Comp", OriginYear = 1992, DevelopmentYear = 1993, Incremental = 85.0},
            //    new InputData { Product = "Non-Comp", OriginYear = 1993, DevelopmentYear = 1993, Incremental = 100.0}
            //};


            // first check 1990 @ development year 1
            // 4 origins with 4 devleopment years

        }

        

        

        class Product
        {
            public string Name { get; set; }
            public List<double> Values { get; set; }
        }
    }
}
