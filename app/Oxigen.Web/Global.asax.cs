

using Elmah.Contrib.Mvc;
using Oxigen.ApplicationServices;
using Oxigen.Core.Installer;
using Oxigen.Web.Controllers.ModelBinders;

namespace Oxigen.Web
{
    using System;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Castle.Windsor;

    using CommonServiceLocator.WindsorAdapter;

    using log4net.Config;

    using Microsoft.Practices.ServiceLocation;

    using SharpArch.Core.NHibernateValidator.ValidatorProvider;
    using SharpArch.Data.NHibernate;
    using SharpArch.Web.Areas;
    using SharpArch.Web.Castle;
    using SharpArch.Web.ModelBinder;
    using SharpArch.Web.NHibernate;

    using Oxigen.Data.NHibernateMaps;
    using Oxigen.Web.CastleWindsor;
    using Oxigen.Web.Controllers;

    //// Note: For instructions on enabling IIS6 or IIS7 classic mode,
    //// visit http://go.microsoft.com/?LinkId=9394801

    public class OxigenApplication : HttpApplication
    {
        #region Constants and Fields

        private WebSessionStorage webSessionStorage;

        #endregion

        #region Public Methods

        public override void Init()
        {
            base.Init();

            // The WebSessionStorage must be created during the Init() to tie in HttpApplication events
            this.webSessionStorage = new WebSessionStorage(this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Due to issues on IIS7, the NHibernate initialization cannot reside in Init() but
        /// must only be called once.  Consequently, we invoke a thread-safe singleton class to
        /// ensure it's only initialized once.
        /// </summary>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            NHibernateInitializer.Instance().InitializeNHibernateOnce(this.InitializeNHibernateSession);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Useful for debugging
            Exception ex = this.Server.GetLastError();
            var reflectionTypeLoadException = ex as ReflectionTypeLoadException;
        }

        protected void Application_Start()
        {
            XmlConfigurator.Configure();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new AreaViewEngine());

            ModelBinders.Binders.DefaultBinder = new SharpModelBinder();


            ModelValidatorProviders.Providers.Add(new NHibernateValidatorProvider());

            this.InitializeServiceLocator();

            ModelBinders.Binders.Add(typeof(InstallerSetup), new InsallerSetupBinder(ServiceLocator.Current.GetInstance<IChannelManagementService>()));

            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RouteRegistrar.RegisterRoutesTo(RouteTable.Routes);
        }

        /// <summary>
        /// Instantiate the container and add all Controllers that derive from
        /// WindsorController to the container.  Also associate the Controller
        /// with the WindsorContainer ControllerFactory.
        /// </summary>
        protected virtual void InitializeServiceLocator()
        {
            IWindsorContainer container = new WindsorContainer();
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));

            container.RegisterControllers(typeof(HomeController).Assembly);
            ComponentRegistrar.AddComponentsTo(container);

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }

        /// <summary>
        /// If you need to communicate to multiple databases, you'd add a line to this method to
        /// initialize the other database as well.
        /// </summary>
        private void InitializeNHibernateSession()
        {
            NHibernateSession.Init(
                this.webSessionStorage, 
                new[] { this.Server.MapPath("~/bin/Oxigen.Data.dll") }, 
                new AutoPersistenceModelGenerator().Generate());
            
        }


        #endregion

        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new ElmahHandleErrorAttribute());
            filters.Add(new HandleErrorAttribute());
        }

    }
}