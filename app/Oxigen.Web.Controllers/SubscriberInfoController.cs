using System;
using System.Web;
using System.Web.Mvc;
using Oxigen.Web.Controllers.ActionFilterAttributes;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.Demographic;
using OxigenIIAdvertising.Services;
using OxigenIIAdvertising.XMLSerializer;
using log4net;

namespace Oxigen.Web.Controllers
{
    [HandleError]
    public class SubscriberInfoController : Controller
    {
        private ILog _logger = LogManager.GetLogger("Logger1");
        [DynamicContent(MaxAgeInSeconds = 10800)] // 3 hours
        public ActionResult Subscriptions(string id)
        {
            ChannelSubscriptions subscriptions = (new DAService("Oxigen")).GetUserChannelSubscriptions(null, id); // for backwards compatibility, unused userGUID param is set to null instead of creating another method and propagating it to the WCF contracts.

            if (subscriptions.SubscriptionSet == null)
                return HttpNotFound();

            byte[] subscriptionsInBytes = Serializer.SerializeClearToByteArray(subscriptions);
            var fileContentResult = new FileContentResult(subscriptionsInBytes, System.Net.Mime.MediaTypeNames.Application.Octet);
            fileContentResult.FileDownloadName = "ss_channel_subscription_data.dat";
            return fileContentResult;
        }

        [DynamicContent(MaxAgeInSeconds = 86400)] // 1 day
        public ActionResult DemographicData(string id)
        {
            DemographicData demographicData = (new DAService("Oxigen")).GetUserDemographicData(id);

            if (demographicData.Gender == null)
                return HttpNotFound();

            byte[] demographicDataInBytes = Serializer.SerializeClearToByteArray(demographicData);
            var fileContentResult = new FileContentResult(demographicDataInBytes, System.Net.Mime.MediaTypeNames.Application.Octet);
            fileContentResult.FileDownloadName = "ss_demo_data.dat";
            return fileContentResult;
        }

        [HttpPost]
        public ActionResult Heartbeat(string id)
        {
            try
            {
                (new DAService("Oxigen")).SetHeartBeat(id);
            }
            catch(Exception ex)
            {
                _logger.Error(ex.ToString());
                throw;
            }
            return new EmptyResult();
        }
    }
}
