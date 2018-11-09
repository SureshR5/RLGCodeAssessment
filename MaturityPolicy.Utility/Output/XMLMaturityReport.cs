namespace MaturityPolicy.Utility.Output
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    using MaturityPolicy.Utility.Model;

    /// <summary>
    /// Class to generate maturity report xml
    /// </summary>
    public class XMLMaturityReport : IMaturityReport
    {
        #region Public methods

        /// <summary>
        /// Generate the xml file.
        /// </summary>
        /// <param name="maturityDetails"></param>
        /// <returns>Stream result</returns>
        public Stream Generate(List<MaturityDetail> maturityDetails)
        {
            if (maturityDetails == null || !maturityDetails.Any())
            {
                // Throws a exception when maturity details are not available and we can log the exception here for future use.
                throw new ArgumentNullException("Maturity Details are not available.");
            }

            XmlSerializer serializer = new XmlSerializer(maturityDetails.GetType());

            MemoryStream stream = new MemoryStream();

            serializer.Serialize(stream, maturityDetails);

            stream.Position = 0;

            return stream;
        }

        #endregion
    }
}
