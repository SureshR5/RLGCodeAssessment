namespace MaturityPolicy.Web.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    using MaturityPolicy.Utility;

    /// <summary>
    /// Home controller.
    /// </summary>
    public class HomeController : Controller
    {
        #region Constants

        /// <summary>
        /// Allowed file extention.
        /// </summary>
        private const string allowedFileExtension = ".csv";

        /// <summary>
        /// Report file name. 
        /// </summary>
        private const string reportFileName = "MaturityPolicyDetails.xml";

        #endregion

        #region Private variables

        /// <summary>
        /// Maturity model interface.
        /// </summary>
        private IMaturityModel maturityModel;

        /// <summary>
        /// Maturity calculation interface.
        /// </summary>
        private IMaturityCalculation maturityCalculation;

        /// <summary>
        /// Maturity report interface.
        /// </summary>
        private IMaturityReport maturityReport;

        #endregion

        #region Constructor

        /// <summary>
        /// Inialize the constructor.
        /// </summary>
        /// <param name="maturityModel"></param>
        /// <param name="maturityCalculation"></param>
        /// <param name="maturityReport"></param>
        public HomeController(IMaturityModel maturityModel, IMaturityCalculation maturityCalculation, IMaturityReport maturityReport)
        {
            this.maturityModel = maturityModel;
            this.maturityCalculation = maturityCalculation;
            this.maturityReport = maturityReport;
        }

        #endregion

        #region ActionResult methods

        /// <summary>
        /// Get method to display the view.
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Post method to process the CSV data and returns the maturity policies report as xml.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Report Xml file.</returns>
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (file == null || file.ContentLength < 0)
            {
                ModelState.AddModelError("File", "Please Upload Your file");
                return View();
            }

            if (!file.FileName.EndsWith(allowedFileExtension))
            {
                ModelState.AddModelError("FileType", "Please upload the file of type: " + allowedFileExtension);
                return View();
            }

            try
            {
                ModelState.Clear();

                ViewBag.Message = "File uploaded successfully";

                var policyDetails = this.maturityModel.RetrievePolicyDetails(file.InputStream);

                var fee = this.maturityModel.RetrieveManagementFee();

                var maturityDetails = this.maturityCalculation.Calculate(policyDetails, fee);

                var maturityReportStream = this.maturityReport.Generate(maturityDetails);

                return File(maturityReportStream, "application/xml", reportFileName);
            }
            catch (ArgumentNullException ex)
            {
                // Throws a exception when maturity details are not available and we can log the exception here for future use.
                ModelState.AddModelError("Unable to process the file", ex.Message);
                return View();
            }
        }
    }

    #endregion
}