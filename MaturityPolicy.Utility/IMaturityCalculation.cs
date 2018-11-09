namespace MaturityPolicy.Utility
{
    using System.Collections.Generic;

    using MaturityPolicy.Utility.Model;

    /// <summary>
    /// Interface for maturity calculation.
    /// </summary>
    public interface IMaturityCalculation
    {
        /// <summary>
        /// Calculate the maturity value for policy.
        /// </summary>
        /// <param name="policyDetails"></param>
        /// <param name="managementFee"></param>
        /// <returns>List of maturity detail</returns>
        List<MaturityDetail> Calculate(List<PolicyDetail> policyDetails, Dictionary<PolicyType, double> managementFee);
    }
}
