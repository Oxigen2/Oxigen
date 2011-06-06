using System.Web.Routing;
using MvcContrib.TestHelper;
using NUnit.Framework;
using Oxigen.Web.Assets;
using AssertionException = MvcContrib.TestHelper.AssertionException;

namespace Tests.Oxigen.Web.Assets
{
  [TestFixture]
  public class RouteRegistrarTests
  {
    #region Public Methods

    [SetUp]
    public void SetUp()
    {
      RouteTable.Routes.Clear();
      RouteRegistrar.RegisterRoutesTo(RouteTable.Routes);
    }
    
    [Test]
    public void CanVerifyRouteMaps()
    {
      "~/slide/GUID_A.jpg".Route().ShouldMapTo<ServecontentController>(x => x.Slide("GUID_A.jpg"));
      "~/advert/GUID_A.jpg".Route().ShouldMapTo<ServecontentController>(x => x.Advert("GUID_A.jpg"));
    }

    #endregion
  }
}
