namespace MaturityPolicy.Web.Installers
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using MaturityPolicy.Utility;
    using MaturityPolicy.Utility.Calculation;
    using MaturityPolicy.Utility.DataSource;
    using MaturityPolicy.Utility.Output;

    /// <summary>
    /// Service installer.
    /// </summary>
    public class ServiceInstaller : IWindsorInstaller
    {
        #region Public methods
        
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                 Component
                 .For<IMaturityCalculation>()
                 .ImplementedBy<MaturityCalculation>()
                 .LifestylePerWebRequest());
            container.Register(
                 Component
                 .For<IMaturityModel>()
                 .ImplementedBy<CSVMaturityModel>()
                 .LifestylePerWebRequest());
            container.Register(
                 Component
                 .For<IMaturityReport>()
                 .ImplementedBy<XMLMaturityReport>()
                 .LifestylePerWebRequest());
        }

        #endregion
    }
}