namespace MaturityPolicy.Web
{
    using System.Web.Mvc;
    using Castle.Windsor;
    using Castle.Windsor.Installer;
    using MaturityPolicy.Web.Infrastructure;

    /// <summary>
    /// Class for IocContainer
    /// </summary>
    public static class IocContainer
    {
        #region Private variable

        /// <summary>
        ///  Container vaiable.
        /// </summary>
        private static IWindsorContainer _container;

        #endregion

        #region Public methods

        /// <summary>
        /// Setup the container.
        /// </summary>
        public static void Setup()
        {
            _container = new WindsorContainer().Install(FromAssembly.This());

            WindsorControllerFactory controllerFactory = new WindsorControllerFactory(_container.Kernel);

            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        #endregion
    }
}