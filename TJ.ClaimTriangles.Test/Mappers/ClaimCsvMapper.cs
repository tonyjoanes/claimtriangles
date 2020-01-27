using System;
using System.Collections.Generic;
using System.Text;
using TJ.ClaimTriangles.Implementation;
using Xunit;

namespace TJ.ClaimTriangles.Test.Mappers
{
    public class ClaimCsvMapperTests
    {
        [Fact]
        public void Map_WithSingleRow_MapsWithoutError()
        {
            var dataList = new List<string[]>();

            dataList.Add(new string[] 
            { 
                "Product-Name" ,
                "1990",
                "1992",
                "100"
            });

            var actual = ClaimCsvMapper.Map(dataList);

            Assert.True(actual.Count == 1);
            Assert.Equal("Product-Name", actual[0].Product);
            Assert.Equal(1990, actual[0].OriginYear);
            Assert.Equal(1992, actual[0].DevelopmentYear);
            Assert.Equal(100, actual[0].Incremental);
        }
    }
}
