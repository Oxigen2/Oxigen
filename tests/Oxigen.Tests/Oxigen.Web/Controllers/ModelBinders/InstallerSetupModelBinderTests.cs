using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oxigen.ApplicationServices;
using Oxigen.Core;
using Rhino.Mocks;
using SharpArch.Testing.NUnit;
using System.Web.Mvc;
using NUnit.Framework;
using Oxigen.Core.Installer;
using Oxigen.Web.Controllers.ModelBinders;
using Tests.Oxigen.Core;

namespace Tests.Oxigen.Web.Controllers.ModelBinders
{
    [TestFixture]
    public class InstallerSetupModelBinderTests
    {
        [Test]
        public void CanBindSingleSubscriptionTest()
        {
            var channelsManagementService =
                MockRepository.GenerateMock<IChannelManagementService>();
            channelsManagementService.Expect(x => x.Get(11)).Return(new Channel() {CategoryID = 11, ChannelName = "Arsenal", ChannelGUID = "ArsenalGUID"});
            var dict = new ValueProviderDictionary(null) {   
                     { "subscription", new ValueProviderResult(null,"11-Arsenal",null) }
                 }; 
            var bindingContext = new ModelBindingContext() { ValueProvider = dict };
            var binder = new InsallerSetupBinder(channelsManagementService);
            var setupFile = (InstallerSetup)binder.BindModel(null, bindingContext);

            setupFile.GetSetupText().ShouldEqual("11,,ArsenalGUID,,Arsenal,,10\r\n");
        }

        [Test]
        public void CanBindMultipleSubscriptionTest()
        {
            var channelsManagementService =
                MockRepository.GenerateMock<IChannelManagementService>();
            channelsManagementService.Expect(x => x.Get(11)).Return(new Channel() { CategoryID = 11, ChannelName = "Arsenal", ChannelGUID = "ArsenalGUID" });
            channelsManagementService.Expect(x => x.Get(22)).Return(new Channel() { CategoryID = 22, ChannelName = "Leeds", ChannelGUID = "LeedsGUID" });
            var dict = new ValueProviderDictionary(null) {   
                     { "subscription", new ValueProviderResult(null,"11-Arsenal+22-Leeds",null) }
                 };
            var bindingContext = new ModelBindingContext() { ValueProvider = dict };
            var binder = new InsallerSetupBinder(channelsManagementService);
            var setupFile = (InstallerSetup)binder.BindModel(null, bindingContext);

            setupFile.GetSetupText().ShouldEqual("11,,ArsenalGUID,,Arsenal,,10\r\n22,,LeedsGUID,,Leeds,,10\r\n");
        }

        [Test]
        public void CanBindMultipleSubscriptionWithWeightingTest()
        {
            var channelsManagementService =
                MockRepository.GenerateMock<IChannelManagementService>();
            channelsManagementService.Expect(x => x.Get(11)).Return(new Channel() { CategoryID = 11, ChannelName = "Arsenal", ChannelGUID = "ArsenalGUID" });
            channelsManagementService.Expect(x => x.Get(22)).Return(new Channel() { CategoryID = 22, ChannelName = "Leeds", ChannelGUID = "LeedsGUID" });
            var dict = new ValueProviderDictionary(null) {   
                     { "subscription", new ValueProviderResult(null,"11.30-Arsenal+22.40-Leeds",null) }
                 };
            var bindingContext = new ModelBindingContext() { ValueProvider = dict };
            var binder = new InsallerSetupBinder(channelsManagementService);
            var setupFile = (InstallerSetup)binder.BindModel(null, bindingContext);

            setupFile.GetSetupText().ShouldEqual("11,,ArsenalGUID,,Arsenal,,30\r\n22,,LeedsGUID,,Leeds,,40\r\n");
        }

        [Test]
        public void CanBindMultipleSubscriptionWithMultiWordNameTest()
        {
            var channelsManagementService =
                MockRepository.GenerateMock<IChannelManagementService>();
            channelsManagementService.Expect(x => x.Get(22)).Return(new Channel() { CategoryID = 22, ChannelName = "Leeds United", ChannelGUID = "LeedsGUID" });
            var dict = new ValueProviderDictionary(null) {   
                     { "subscription", new ValueProviderResult(null,"22-Leeds-United",null) }
                 };
            var bindingContext = new ModelBindingContext() { ValueProvider = dict };
            var binder = new InsallerSetupBinder(channelsManagementService);
            var setupFile = (InstallerSetup)binder.BindModel(null, bindingContext);

            setupFile.GetSetupText().ShouldEqual("22,,LeedsGUID,,Leeds United,,10\r\n");
        }
    }
}
