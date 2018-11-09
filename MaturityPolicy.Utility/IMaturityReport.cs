namespace MaturityPolicy.Utility
{
    using System.Collections.Generic;
    using System.IO;

    using MaturityPolicy.Utility.Model;

    /// <summary>
    /// Interface for maturity details report.
    /// </summary>
    public interface IMaturityReport
    {
        /// <summary>
        /// Generate a maturity details report.
        /// </summary>
        /// <param name="maturityDetails"></param>
        /// <returns>Stream of maturity detail list</returns>
        Stream Generate(List<MaturityDetail> maturityDetails);
    }
}
