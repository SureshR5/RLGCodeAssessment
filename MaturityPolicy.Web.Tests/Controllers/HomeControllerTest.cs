namespace MaturityPolicy.Web.Tests.Controllers
{     
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    using MaturityPolicy.Utility;
    using MaturityPolicy.Utility.Model;
    using MaturityPolicy.Web.Controllers;

    /// <summary>
    /// Test class for home controller
    /// </summary>
    [TestClass]
    public class HomeControllerTest
    {

        private HomeController controller;

        #region Init Test

        [TestInitialize]
        [Description("Test Initialize")]
        public void InitTest()
        {
            var mockMaturityModel = new Mock<IMaturityModel>();

            mockMaturityModel.Setup(s => s.RetrieveManagementFee())
            .Returns(this.RetrieveManagementFee());

            mockMaturityModel.Setup(s => s.RetrievePolicyDetails(It.IsAny<Stream>()))
           .Returns(this.GetPolicyDetails());

            var mockMaturityCalculation = new Mock<IMaturityCalculation>();

            mockMaturityCalculation.Setup(s => s.Calculate(It.IsAny<List<PolicyDetail>>(), It.IsAny<Dictionary<PolicyType, double>>()))
            .Returns(this.GetMaturityDetails());

            var mockMaturityReport = new Mock<IMaturityReport>();

            mockMaturityReport.Setup(s => s.Generate(It.IsAny<List<MaturityDetail>>()))
            .Returns(this.GetStreamContent());

            controller = new HomeController(mockMaturityModel.Object, mockMaturityCalculation.Object, mockMaturityReport.Object);

        }

        #endregion

        #region Test methods

        [TestMethod]
        [Description("Get Index method returns the view result as expected")]
        public void GetIndexMethod_returns_the_view_result_as_expected()
        {            
            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [Description("Post Index method returns the view result when model state is invalid")]
        public void PostIndex_method_returns_the_view_result_when_model_state_is_invalid()
        {
            // Arrange
            controller.ModelState.AddModelError("test", "test");
            var uploadedFile = new Mock<HttpPostedFileBase>();

            uploadedFile
                .Setup(f => f.ContentLength)
                .Returns(10);

            uploadedFile
                .Setup(f => f.FileName)
                .Returns("MaturityData.csv");

            uploadedFile
                .Setup(f => f.InputStream)
                .Returns(this.GetStreamContent());

            // Act
            var result = controller.Index(uploadedFile.Object) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [Description("Post Index method returns the view result when file is null")]
        public void PostIndex_method_returns_the_view_result_when_file_is_null()
        {
            // Arrange
            var uploadedFile = new Mock<HttpPostedFileBase>();

            uploadedFile
                .Setup(f => f.ContentLength)
                .Returns(10);

            uploadedFile
                .Setup(f => f.FileName)
                .Returns("MaturityData.txt");

            uploadedFile
                .Setup(f => f.InputStream)
                .Returns(this.GetStreamContent());

            // Act
            var result = controller.Index(uploadedFile.Object) as ViewResult;

            // Assert
            Assert.IsNotNull(result);

            var modelState = controller.ModelState;

            Assert.AreEqual(1, modelState.Keys.Count);

            Assert.IsTrue(modelState.Keys.Contains("FileType"));

            Assert.IsTrue(modelState["FileType"].Errors.Count == 1);
        }

        [TestMethod]
        [Description("Post Index method returns the view result when file extention is not matched")]
        public void PostIndex_method_returns_the_view_result_when_file_extention_is_not_matched()
        {
            // Act
            var result = controller.Index(null) as ViewResult;
            var modelState = controller.ModelState;

            // Assert
            Assert.IsNotNull(result);

            Assert.AreEqual(1, modelState.Keys.Count);

            Assert.IsTrue(modelState.Keys.Contains("File"));

            Assert.IsTrue(modelState["File"].Errors.Count == 1);
        }

        [TestMethod]
        [Description("Post Index method returns the file result")]
        public void PostIndex_method_returns_the_view_result()
        {

            // Arrange
            var uploadedFile = new Mock<HttpPostedFileBase>();

            uploadedFile
                .Setup(f => f.ContentLength)
                .Returns(10);

            uploadedFile
                .Setup(f => f.FileName)
                .Returns("MaturityData.csv");

            uploadedFile
                .Setup(f => f.InputStream)
                .Returns(this.GetStreamContent());

            // Act
            var result = controller.Index(uploadedFile.Object) as FileStreamResult;

            // Assert
            Assert.IsNotNull(result);
        }

        #endregion

        #region Private methods

        private Dictionary<PolicyType, double> RetrieveManagementFee()
        {
            var managementFee = new Dictionary<PolicyType, double>
            { {PolicyType.A, 0.03}, {PolicyType.B, 0.05}, {PolicyType.C, 0.07}};

            return managementFee;
        }

        private List<PolicyDetail> GetPolicyDetails()
        {
            var policyDetails = new List<PolicyDetail>{
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

            return policyDetails;
        }

        private Stream GetStreamContent()
        {
            string fileContent = @"A100001,£19,555.00";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));
            return stream;
        }

        private List<MaturityDetail> GetMaturityDetails()
        {

            var maturityDetails = new List<MaturityDetail>{
                new MaturityDetail{
                    PolicyNumber = "A100001",
                    MaturityValue = "£19,555.00"
                },
                new MaturityDetail{
                    PolicyNumber = "B100001",
                    MaturityValue = "£55,555.00"
                },
            };

            return maturityDetails;
        }

        #endregion
    }
}
