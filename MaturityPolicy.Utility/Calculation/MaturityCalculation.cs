namespace MaturityPolicy.Utility.Calculation
{    
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using MaturityPolicy.Utility.Model;

    /// <summary>
    /// Maturity calculation class
    /// </summary>
    public class MaturityCalculation : IMaturityCalculation
    {
        #region Private variables

        /// <summary>
        /// Policy Taken out date.
        /// </summary>
        private static string policyTakenOutDate = "01/01/1990";

        #endregion

        #region Public methods

        /// <summary>
        /// Calculate the maturity value for policy.
        /// </summary>
        /// <param name="policyDetails"></param>
        /// <param name="managementFee"></param>
        /// <returns>List of maturity details</returns>
        public List<MaturityDetail> Calculate(List<PolicyDetail> policyDetails, Dictionary<PolicyType, double> managementFee)
        {

            if (policyDetails == null || !policyDetails.Any())
            {
                // Throws a exception when maturity data's input are not available and we can log the exception here for future use.
                throw new ArgumentNullException("Maturity data's input are not available.");
            }

            List<MaturityDetail> output = new List<MaturityDetail>();

            policyDetails.ForEach(data =>
            {
                MaturityDetail result = new MaturityDetail();

                result.PolicyNumber = data.Id;

                // Method to verify the bonus applicable for the policy based on policy number, membership, policy start date.
                double bonus = ApplyDiscretionaryBonus(data.Type, data.Membership, data.PolicyStartDate, data.DiscretionaryBonus);

                // Calculating the maturity value for the policy.
                result.MaturityValue = CalculateMaturityValue(data.Premiums, managementFee[data.Type], data.UpliftPercentage, bonus);

                // Adding the maturity data to the list.
                output.Add(result);

            });

            return output;

        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method for applying discretionary bonus.
        /// </summary>
        /// <param name="policyType"></param>
        /// <param name="membership"></param>
        /// <param name="policyStartDate"></param>
        /// <param name="discretionaryBonus"></param>
        /// <returns>Bonus for the policy type.</returns>
        private static double ApplyDiscretionaryBonus(PolicyType policyType, bool membership, DateTime policyStartDate, double discretionaryBonus)
        {
            double bonus = 0;

            // Convert the policy taken out date as formatted date.
            DateTime policyTakenOutPeriod = DateTime.ParseExact(policyTakenOutDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            // Based on policy type to verify the bonus is applicable or not.
            switch (policyType)
            {
                case PolicyType.A:
                    // For the policy 'A' type to validate the bonus is applicable based on the policy start date is taken out before the policy taken out period.
                    bonus = policyStartDate < policyTakenOutPeriod ? discretionaryBonus : bonus;
                    break;

                case PolicyType.B:
                    // For the policy 'B' type to validate the bonus is applicable based on the policy membership to be 'Y'.
                    bonus = membership ? discretionaryBonus : bonus;
                    break;

                case PolicyType.C:
                    // For the policy 'C' type to validate the bonus is applicable based on the policy start date is taken out after or on the policy taken out period with the policy membership to be 'Y'.
                    bonus = (policyStartDate >= policyTakenOutPeriod && membership) ? discretionaryBonus : bonus;
                    break;
            }

            return bonus;
        }

        /// <summary>
        /// To calculate the maturity value for the policy.
        /// </summary>
        /// <param name="Premium"></param>
        /// <param name="managementFee"></param>
        /// <param name="upliftPerc"></param>
        /// <param name="discretionaryBonus"></param>
        /// <returns>Maturity value with the formatted money type for the policy.</returns>
        private static string CalculateMaturityValue(double premium, double managementFee, double upliftPerc, double discretionaryBonus)
        {
            return ((premium - (premium * managementFee) + discretionaryBonus) * upliftPerc).ToString("C", CultureInfo.CreateSpecificCulture("en-GB"));
        }

        #endregion
    }
}
