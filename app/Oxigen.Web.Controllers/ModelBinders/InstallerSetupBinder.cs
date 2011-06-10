using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Oxigen.ApplicationServices;
using Oxigen.Core;
using Oxigen.Core.Installer;

namespace Oxigen.Web.Controllers.ModelBinders
{
    public class InsallerSetupBinder : IModelBinder
    {
        private readonly IChannelManagementService _channelManagementService;

        public InsallerSetupBinder(IChannelManagementService channelManagementService)
        {
            _channelManagementService = channelManagementService;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult value = bindingContext.ValueProvider.GetValue("subscription");

            string serializedSubscription = value.AttemptedValue;
            var subscriptions = serializedSubscription.Split('|');
            var setupFile = new InstallerSetup();
            foreach (var subscription in subscriptions)
            {
                var values = subscription.Split('-');
                var idAndWeighting = values[0].Split('.');
                int channelId = int.Parse(idAndWeighting[0]);
                int weighting = (idAndWeighting.Length == 2) ? int.Parse(idAndWeighting[1]) : Channel.DefaultWeighting;
                var channel = _channelManagementService.Get(channelId);

                setupFile.Add(channelId, channel.ChannelGUID, channel.ChannelName, weighting);
            }
            return setupFile;
        }



    }
}
