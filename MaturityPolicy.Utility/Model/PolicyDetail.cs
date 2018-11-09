namespace MaturityPolicy.Utility.Model
{
    using System;

    #region Public properties

    /// <summary>
    /// The contract class for processing policy detail.
    /// </summary>
    public class PolicyDetail
    {
        /// <summary>
        /// Gets or sets a value for policy Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a value for policy type.
        /// </summary>
        public PolicyType Type { get; set; }

        /// <summary>
        ///  Gets or sets a value for policy start date.
        /// </summary>
        public DateTime PolicyStartDate { get; set; }

        /// <summary>
        ///  Gets or sets a value for policy premiums.
        /// </summary>
        public double Premiums { get; set; }

        /// <summary>
        ///  Gets or sets a value for policy membership.
        /// </summary>
        public Boolean Membership { get; set; }

        /// <summary>
        ///  Gets or sets a value for policy discretionary bonus.
        /// </summary>
        public double DiscretionaryBonus { get; set; }

        /// <summary>
        ///  Gets or sets a value for policy uplift percentage.
        /// </summary>
        public double UpliftPercentage { get; set; }
    }

    #endregion
}
