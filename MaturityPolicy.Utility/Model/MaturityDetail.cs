namespace MaturityPolicy.Utility.Model
{
    using System.Xml.Serialization;   

    /// <summary>
    /// The contract class for maturity data output.
    /// </summary>
    [XmlType("MaturityDetail")]
    public class MaturityDetail
    {
        #region Public properties

        /// <summary>
        /// Gets or sets a value for member policy number.
        /// </summary>
        public string PolicyNumber { get; set; }

        /// <summary>
        /// Gets or sets a maturity value.
        /// </summary>
        public string MaturityValue { get; set; }

        #endregion
    }
}
