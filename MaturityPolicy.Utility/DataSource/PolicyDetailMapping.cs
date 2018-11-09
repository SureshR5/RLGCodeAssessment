namespace MaturityPolicy.Utility.DataSource.Mapping
{
    using System;
    using System.Globalization;
    using CsvHelper.Configuration;

    using MaturityPolicy.Utility.Model;

    /// <summary>
    /// Policy detail mapping.
    /// </summary>
    public sealed class PolicyDetailMapping: ClassMap<PolicyDetail>
    {
        #region Constructor

        /// <summary>
        /// Initialize the mapping for csv file.
        /// </summary>
        public PolicyDetailMapping()
        {
            this.Map(m => m.Id).Name("policy_number");

            // Get the policy type based on policy number first character.
            this.Map(m => m.Type).Name("policy_number").ConvertUsing((row =>
            {
                var policyType = row.GetField<string>("policy_number")[0];
                return (PolicyType)Enum.Parse(typeof(PolicyType), policyType.ToString());               
            }));

            this.Map(m => m.PolicyStartDate).Name("policy_start_date").ConvertUsing((row =>
            {
                var startDate = row.GetField<string>("policy_start_date");
                return DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);                
            }));           

            this.Map(m => m.Premiums).Name("premiums");

            this.Map(m => m.Membership).Name("membership")
                .TypeConverterOption.BooleanValues(true, true, "Y")
                .TypeConverterOption.BooleanValues(false, true, "N");

            this.Map(m => m.DiscretionaryBonus).Name("discretionary_bonus");

            // Map the upliftpercentage member to converting the percentage as specified.
            this.Map(m => m.UpliftPercentage).Name("uplift_percentage").ConvertUsing(row => (1 + (row.GetField<double>("uplift_percentage") / 100)));
        }

        #endregion
    }
}
