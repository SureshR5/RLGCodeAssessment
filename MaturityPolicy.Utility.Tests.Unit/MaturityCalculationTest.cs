namespace MaturityPolicy.Utility.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MaturityPolicy.Utility.Calculation;
    using MaturityPolicy.Utility.Model;
    
    /// <summary>
    /// Test classs for Maturity calculation
    /// </summary>
    [TestClass]
    public class MaturityCalculationTest
    {
        private IMaturityCalculation maturityCalculation;

        private List<PolicyDetail> policyDetails;

        private Dictionary<PolicyType, double> managementFee;

        #region Test Initialize

        [TestInitialize]
        [Description("Test Iniatialize")]
        public void InitTest()
        {
            maturityCalculation = new MaturityCalculation();

            policyDetails = new List<PolicyDetail>{
                new PolicyDetail{
                    Id = "A100001",
                    Type = PolicyType.A,
                    PolicyStartDate = DateTime.Parse("01/06/1986"),
                    Premiums = 10000,
                    Membership = true,
                    DiscretionaryBonus = 1000,
                    UpliftPercentage = 1.4
                },
                new PolicyDetail{
                    Id = "A100001",
                    Type = PolicyType.A,
                    PolicyStartDate = DateTime.Parse("01/06/1986"),
                    Premiums = 10000,
                    Membership = true,
                    DiscretionaryBonus = 1000,
                    UpliftPercentage = 1.4
                }
            };

            managementFee = new Dictionary<PolicyType, double>
            { {PolicyType.A, 0.03}, {PolicyType.B, 0.05}, {PolicyType.C, 0.07}};

        }

        #endregion

        #region Test methods
      
        [TestMethod]
        [Description("Calculate method throws argument null exception when the policy details list empty")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalculateMethod_throws_argument_null_exception()
        {
            // Act
            maturityCalculation.Calculate(null, this.managementFee);
        }


        [TestMethod]
        [Description("Calculate method returns the maturity detail list as expected")]
        public void CalculateMethod_returns_the_maturity_detail_as_expected()
        {
            // Act
            var maturityDetails = maturityCalculation.Calculate(this.policyDetails, this.managementFee);

            // Assert
            Assert.AreEqual(maturityDetails.Count, 2);
        }

        #endregion
    }
}
