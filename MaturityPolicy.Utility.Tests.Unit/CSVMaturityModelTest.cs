namespace MaturityPolicy.Utility.Tests.Unit
{   
    using System.IO;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MaturityPolicy.Utility.DataSource;
    using MaturityPolicy.Utility.Model;

    /// <summary>
    /// Test classs for CSVMaturityModel
    /// </summary>
    [TestClass]
    public class CSVMaturityModelTest
    {
        private IMaturityModel maturityModel;

        private MemoryStream stream;

        private StringBuilder fileContent;

        #region Test Initialize

        [TestInitialize]
        [Description("Test Iniatialize")]
        public void InitTest()
        {
            SetFileContent();

            maturityModel = new CSVMaturityModel();
        }

        #endregion

        #region Test methods

        [TestMethod]
        [Description("RetrievePolicyDetails method returns the policy detail list as expected")]
        public void RetrievePolicyDetailsMethod_returns_the_policy_detail_list_as_expected()
        {
            // Arrange
            this.stream = new MemoryStream(Encoding.UTF8.GetBytes(this.fileContent.ToString()));

            // Act
            var policyDetails = maturityModel.RetrievePolicyDetails(this.stream);

            this.stream.Dispose();

            // Assert
            Assert.IsNotNull(policyDetails);
            Assert.AreEqual(policyDetails.Count, 2);
            Assert.AreEqual(policyDetails[0].Id, "A100001");
            Assert.AreEqual(policyDetails[1].Id, "B100001");
            Assert.AreEqual(policyDetails[0].Type, PolicyType.A);
            Assert.AreEqual(policyDetails[1].Type, PolicyType.B);
        }


        [TestMethod]
        [Description("RetrieveManagementFee method returns the key value pairs as expected")]
        public void RetrieveManagementFeeMethod_returns_the_key_value_pairs_as_expected()
        {
            // Act
            var managementFeeDetails = maturityModel.RetrieveManagementFee();

            // Assert
            Assert.IsNotNull(managementFeeDetails);
            Assert.AreEqual(managementFeeDetails.Count, 3);
            Assert.AreEqual(managementFeeDetails[PolicyType.A], 0.03);
            Assert.AreEqual(managementFeeDetails[PolicyType.B], 0.05);
            Assert.AreEqual(managementFeeDetails[PolicyType.C], 0.07);
        }

        #endregion

        #region Private methodd

        /// <summary>
        /// Set file content.
        /// </summary>
        private void SetFileContent()
        {
            fileContent = new StringBuilder();
            fileContent.Append("policy_number,policy_start_date,premiums,membership,discretionary_bonus,uplift_percentage");
            fileContent.AppendLine();
            fileContent.Append("A100001,01/06/1986,10000,Y,1000,40").AppendLine();
            fileContent.Append("B100001,01/01/1995,12000,Y,2000,41").AppendLine();
        }

        #endregion
    }
}
