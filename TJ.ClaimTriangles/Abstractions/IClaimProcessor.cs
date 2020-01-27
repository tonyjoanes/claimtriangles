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
        /// Convert and caluate input data into the calculated output.
        /// Handles the complete process of import, accumulating and outputting
        /// the data into a CSV file
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void Invoke(string inputFile, string outPutFile);
    }
}
