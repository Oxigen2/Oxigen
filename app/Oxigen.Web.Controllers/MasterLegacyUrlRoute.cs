using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;

namespace Oxigen.Web.Controllers
{
    public class MasterLegacyUrlRoute : RouteBase
    {
        private Dictionary<string, string> _redirectMap;

        public MasterLegacyUrlRoute() : base()
        {
            _redirectMap = new Dictionary<string, string>()
                                   {
                            {"/downloads/ArsenalFCLiveScreensaver.exe", "7274-Arsenal-FC"},
                            {"/downloads/AstonVillaLiveScreensaver.exe", "7300-Aston-Villa-FC"},
                            {"/downloads/ChelseaScreensaver.exe", "7267-Chelsea-FC"},
                            {"/downloads/ClubBruggeLiveScreensaver.exe", "7278-Club-Brugge-KV"},
                            {"/downloads/FCUtrechtLiveScreensaver.exe", "7279-FC-Utrecht"},
                            {"/downloads/FulhamFCInstaller.exe", "7276-Fulham-FC"},
                            {"/downloads/LeedsUnitedScreensaver.exe", "7273-Leeds-United"},
                            {"/downloads/MCFCScreensaver.exe", "7298-Manchester-City"},
                            {"/downloads/MiddlesbroughFCScreensaver.exe", "7275-Middlesbrough-FC"},
                            {"/downloads/RSCAScreensaver.exe", "7277-RSC-Anderlecht"},
                            {"/downloads/SunderlandAFCInstaller.exe", "7299-Sunderland-AFC"},
                            {"/downloads/TottenhamHotspurScreensaver.exe", "7266-Tottenham-Hotspur"},
                            {"/downloads/SpursScreensaver.exe", "7266-Tottenham-Hotspur"},
                            {"/downloads/WestHamUnitedScreensaver.exe", "7268-West-Ham-United"},
                            {"/downloads/AnaIvanovicLiveScreensaver.exe", "7296-Ana-Ivanovic"},
                            {"/downloads/ATPWorldTourLiveScreensaver.exe", "7286-ATP-World-Tour"},
                            {"/downloads/BangLiveScreensaver.exe", "7308-Bang-Goes-The-Theory"},
                            {"/downloads/CoastTVLiveScreensaver.exe", "7307-Coast"},
                            {"/downloads/DeutscheStiftungDenkmalschutzLiveScreensaver.exe", "7330-German-Monuments"},
                            {"/downloads/MotoGPScreensaver.exe", "7302-MotoGP"},
                            {"/downloads/DartsInstaller.exe", "7301-Professional-Darts"},
                            {"/downloads/UKAthletics2008Screensaver.exe", "7248-UK-Athletics"},
                            {"/downloads/WorldSnookerInstaller.exe", "7297-World-Snooker"},
                            {"/downloads/WorldSnookerChineseInstaller.exe", "7297-World-Snooker"},
                            {"/downloads/BathRugbyLiveScreensaver.exe", "7283-Bath-Rugby"},
                            {"/downloads/CardiffBluesLiveScreensaver.exe", "7694-Cardiff-Blues"},
                            {"/downloads/EnglandRugbyScreensaver.exe", "7293-England-Rugby"},
                            {"/downloads/GloucesterRugbyLiveScreensaver.exe", "7288-Gloucester-Rugby"},
                            {"/downloads/HarlequinsRFCInstaller.exe", "7295-Harlequins-Rugby-Club"},
                            {"/downloads/LeedsCarnegieScreensaver.exe", "7281-Leeds-Carnegie"},
                            {"/downloads/LeedsRhinosInstaller.exe", "7280-Leeds-Rhinos"},
                            {"/downloads/LeinsterRugbyLiveScreensaver.exe", "7285-Leinster-Rugby"},
                            {"/downloads/LondonIrishRFCInstaller.exe", "7292-London-Irish"},
                            {"/downloads/WaspsInstaller.exe", "7290-London-Wasps"},
                            {"/downloads/MunsterRugbyLiveScreensaver.exe", "7291-Munster-Rugby"},
                            {"/downloads/NewcastleFalconsScreensaver.exe", "7287-Newcastle-Falcons"},
                            {"/downloads/NorthamptonSaintsScreensaver.exe", "7289-Northampton-Saints"},
                            {"/downloads/OspreysRugbyLiveScreensaver.exe", "7294-Ospreys-Rugby"},
                            {"/downloads/SaracensLiveScreensaver.exe", "7282-Saracens-Rugby"},
                            {"/downloads/WorcesterRFCInstaller.exe", "7348-Worcester-Warriors"},
                            {"/downloads/ArizonaSuperScreensaver.exe", "7249-Arizona-Cardinals"},
                            {"/downloads/AtlantaSuperScreensaver.exe", "7250-Atlanta-Falcons"},
                            {"/downloads/BaltimoreSuperScreensaver.exe", "7251-Baltimore-Ravens"},
                            {"/downloads/BuffaloSuperScreensaver.exe", "7252-Buffalo-Bills"},
                            {"/downloads/CarolinaSuperScreensaver.exe", "7253-Carolina-Panthers"},
                            {"/downloads/ChicagoSuperScreensaver.exe", "7254-Chicago-Bears"},
                            {"/downloads/CincinnatiSuperScreensaver.exe", "7255-Cincinnati-Bengals"},
                            {"/downloads/ClevelandSuperScreensaver.exe", "7256-Cleveland-Browns"},
                            {"/downloads/DallasSuperScreensaver.exe", "7257-Dallas-Cowboys"},
                            {"/downloads/DenverSuperScreensaver.exe", "7258-Denver-Broncos"},
                            {"/downloads/DetroitSuperScreensaver.exe", "7259-Detroit-Lions"},
                            {"/downloads/GreenBaySuperScreensaver.exe", "7260-Green-Bay-Packers"},
                            {"/downloads/HoustonSuperScreensaver.exe", "7261-Houston-Texans"},
                            {"/downloads/IndianapolisSuperScreensaver.exe", "7262-Indianapolis-Colts"},
                            {"/downloads/JacksonvilleSuperScreensaver.exe", "7263-Jacksonville-Jaguars"},
                            {"/downloads/KansasCitySuperScreensaver.exe", "7264-Kansas-City-Chiefs"},
                            {"/downloads/MiamiSuperScreensaver.exe", "7265-Miami-Dolphins"},
                            {"/downloads/MinnesotaSuperScreensaver.exe", "7355-Minnesota-Vikings"},
                            {"/downloads/NewEnglandSuperScreensaver.exe", "7356-New-England-Patriots"},
                            {"/downloads/NewOrleansSuperScreensaver.exe", "7357-New-Orleans-Saints"},
                            {"/downloads/NYGiantsSuperScreensaver.exe", "7358-New-York-Giants"},
                            {"/downloads/NYJetsSuperScreensaver.exe", "7359-New-York-Jets"},
                            {"/downloads/OaklandSuperScreensaver.exe", "7360-Oakland-Raiders"},
                            {"/downloads/PhiladelphiaSuperScreensaver.exe", "7361-Philadelphia-Eagles"},
                            {"/downloads/PittsburghSuperScreensaver.exe", "7362-Pittsburgh-Steelers"},
                            {"/downloads/SanDiegoSuperScreensaver.exe", "7363-San-Diego-Chargers"},
                            {"/downloads/SanFranciscoSuperScreensaver.exe", "7364-San-Francisco-49ers"},
                            {"/downloads/SeattleSuperScreensaver.exe", "7365-Seattle-Seahawks"},
                            {"/downloads/StLouisSuperScreensaver.exe", "7366-St-Louis-Rams"},
                            {"/downloads/TampaBaySuperScreensaver.exe", "7367-Tampa-Bay-Buccaneers"},
                            {"/downloads/TennesseeSuperScreensaver.exe", "7368-Tennessee-Titans"},
                            {"/downloads/WashingtonSuperScreensaver.exe", "7369-WashingtonRedskins"}
                                   };
        }
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var request = httpContext.Request;
            if (request.Headers["Host"] == "master.oxigen.net")
//            if (request.Headers["Host"] == "localhost:")
            {
                var response = httpContext.Response;
                string mappedUrl;
                if (_redirectMap.TryGetValue(request.Url.AbsolutePath, out mappedUrl))
                {
                    var redirectToUrl = "http://downloads.oxigen.net/installer/" + mappedUrl;
                    response.StatusCode = 301;
                    response.Status = "301 Moved permanently";
                    response.RedirectPermanent(redirectToUrl, true);
                }
                
                response.StatusCode = 404;
                response.Status = "404 Not Found";
                response.End();
            }
            return null;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext,
                    RouteValueDictionary values)
        {
            return null;
        }
    }
}
