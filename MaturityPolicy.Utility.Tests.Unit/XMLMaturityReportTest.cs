namespace MaturityPolicy.Utility.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MaturityPolicy.Utility.Model;
    using MaturityPolicy.Utility.Output;    

    /// <summary>
    /// Test class for XMLMaturityReport.
    /// </summary>
    [TestClass]
    public class XMLMaturityReportTest
    {
        private IMaturityReport maturityReport;

        private List<MaturityDetail> maturityDetails;

        #region Test Initialize

        [TestInitialize]
        [Description("Test Iniatialize")]
        public void InitTest()
        {
            maturityReport = new XMLMaturityReport();

            maturityDetails = new List<MaturityDetail>{
                new MaturityDetail{
                    PolicyNumber = "A100001",
                    MaturityValue = "£19,555.00"
                },
                new MaturityDetail{
                    PolicyNumber = "B100001",
                    MaturityValue = "£55,555.00"
                },
            };

        }

        #endregion

        #region Test methods

        [TestMethod]
        [Description("Generate method throws argument null exception when the maturity details list empty")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateMethod_throws_argument_null_exception()
        {
            // Act
            maturityReport.Generate(null);
        }


        [TestMethod]
        [Description("Generate method returns the maturity detail list as expected")]
        public void GenerateMethod_returns_the_maturity_detail_as_expected()
        {
            // Act
            var streamContent = maturityReport.Generate(this.maturityDetails);

            // Assert
            Assert.AreNotEqual(streamContent.Length, 0);
        }

        #endregion
    }
}
