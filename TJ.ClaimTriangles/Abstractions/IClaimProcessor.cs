namespace TJ.ClaimTriangles
{
    using System.Collections.Generic;
    using TJ.ClaimTriangles.Models;
    
    /// <summary>
    /// The Claim Processor Interface
    /// </summary>
    public interface IClaimProcessor
    {
        /// <summary>
        /// Convert and caluate input data into the calculated output
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        OutputModel GetCalculatedProducts(IEnumerable<InputData> data);
    }
}
