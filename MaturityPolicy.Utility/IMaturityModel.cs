namespace MaturityPolicy.Utility
{
    using System.Collections.Generic;
    using System.IO;

    using MaturityPolicy.Utility.Model;

    /// <summary>
    /// Interface for file type maturity model 
    /// </summary>
    public interface IMaturityModel
    {
        /// <summary>
        /// Retrieve policy details.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>list of policy details</returns>
        List<PolicyDetail> RetrievePolicyDetails(Stream stream);

        /// <summary>
        /// Retrive Management fee.
        /// </summary>
        /// <returns>Management fee details</returns>
        Dictionary<PolicyType, double> RetrieveManagementFee();
    }
}
