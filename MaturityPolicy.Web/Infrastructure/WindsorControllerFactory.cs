namespace MaturityPolicy.Web.Infrastructure
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Castle.MicroKernel;

    /// <summary>
    /// Windsor controller factory.
    /// </summary>
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        #region Private variables

        private readonly IKernel kernel;

        #endregion

        #region Public methods

        public WindsorControllerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            kernel.ReleaseComponent(controller);
        }

        #endregion

        #region Protected method

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
            }
            return (IController)kernel.Resolve(controllerType);
        }

        #endregion
    }
}