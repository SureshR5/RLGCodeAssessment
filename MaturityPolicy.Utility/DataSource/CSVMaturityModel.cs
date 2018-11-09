namespace MaturityPolicy.Utility.DataSource
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CsvHelper;

    using MaturityPolicy.Utility.DataSource.Mapping;
    using MaturityPolicy.Utility.Model;

    /// <summary>
    ///  Class for processing csv file.
    /// </summary>
    public class CSVMaturityModel : IMaturityModel
    {
        #region Public methods

        /// <summary>
        /// Retrieve management fee.
        /// </summary>
        /// <returns>Management fee details.</returns>
        public Dictionary<PolicyType, double> RetrieveManagementFee()
        {
            var managementFee = new Dictionary<PolicyType, double>
            { {PolicyType.A, 0.03}, {PolicyType.B, 0.05}, {PolicyType.C, 0.07}};

            return managementFee;
        }

        /// <summary>
        /// Retrieve policy details.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>Policy details list.</returns>
        public List<PolicyDetail> RetrievePolicyDetails(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                var csvReader = new CsvReader(reader);

                // Csv reader configuration.
                csvReader.Configuration.HasHeaderRecord = true;

                csvReader.Configuration.Delimiter = ",";

                csvReader.Configuration.RegisterClassMap<PolicyDetailMapping>();

                // Csv reader will now read the whole file and return the records as a list.
                return csvReader.GetRecords<PolicyDetail>().ToList();
            }

        }

        #endregion
    }
}
