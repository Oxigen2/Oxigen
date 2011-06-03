namespace Tests.Oxigen.Controllers
{
    using System.Web.Routing;

    using MvcContrib.TestHelper;

    using NUnit.Framework;

    using global::Oxigen.Web.Controllers;

    [TestFixture]
    public class RouteRegistrarTests
    {
        #region Public Methods

        [Test]
        public void CanVerifyRouteMaps()
        {
            "~/installer/XYZ".Route().ShouldMapTo<DownloadController>(x => x.Installer("XYZ"));
            //"~/".Route().ShouldMapTo<HomeController>(x => x.Index());
            
        }

        [SetUp]
        public void SetUp()
        {
            RouteTable.Routes.Clear();
            RouteRegistrar.RegisterRoutesTo(RouteTable.Routes);
        }

        #endregion
    }
}