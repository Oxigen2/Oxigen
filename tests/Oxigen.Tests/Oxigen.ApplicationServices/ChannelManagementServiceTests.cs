using NUnit.Framework;
using Rhino.Mocks;
using SharpArch.Testing.NUnit;
using System.Collections.Generic;
using SharpArch.Core.PersistenceSupport;
using Oxigen.Core;
using Oxigen.ApplicationServices;
using Oxigen.ApplicationServices.ViewModels;
using Oxigen.Core.QueryDtos;
using Oxigen.Core.RepositoryInterfaces;
using Tests.Oxigen.Core;
 

namespace Tests.Oxigen.ApplicationServices
{
    [TestFixture]
    public class channelManagementServiceTests
    {
        [SetUp]
        public void SetUp() {
            ServiceLocatorInitializer.Init();

            channelRepository = 
                MockRepository.GenerateMock<IChannelRepository>();
            channelRepository.Stub(r => r.DbContext)
                .Return(MockRepository.GenerateMock<IDbContext>());
            
            channelManagementService =
                new ChannelManagementService(channelRepository);
        }

        [Test]
        public void CanGetchannel() {
            // Establish Context
            Channel channelToExpect = 
                ChannelInstanceFactory.CreateValidTransientChannel();

            channelRepository.Expect(r => r.Get(1))
                .Return(channelToExpect);

            // Act
            Channel channelRetrieved = 
                channelManagementService.Get(1);

            // Assert
            channelRetrieved.ShouldNotBeNull();
            channelRetrieved.ShouldEqual(channelToExpect);
        }

        [Test]
        public void CanGetAllchannel() {
            // Establish Context
            IList<Channel> channelToExpect = new List<Channel>();

            Channel channel = 
                ChannelInstanceFactory.CreateValidTransientChannel();

            channelToExpect.Add(channel);

            channelRepository.Expect(r => r.GetAll())
                .Return(channelToExpect);

            // Act
            IList<Channel> channelRetrieved =
                channelManagementService.GetAll();

            // Assert
            channelRetrieved.ShouldNotBeNull();
            channelRetrieved.Count.ShouldEqual(1);
            channelRetrieved[0].ShouldNotBeNull();
            channelRetrieved[0].ShouldEqual(channel);
        }

        [Test]
        public void CanGetchannelSummaries() {
            // Establish Context
            IList<ChannelDto> channelSummariesToExpect = new List<ChannelDto>();

            ChannelDto channelDto = new ChannelDto();
            channelSummariesToExpect.Add(channelDto);

            channelRepository.Expect(r => r.GetChannelSummaries())
                .Return(channelSummariesToExpect);

            // Act
            IList<ChannelDto> channelSummariesRetrieved =
                channelManagementService.GetChannelSummaries();

            // Assert
            channelSummariesRetrieved.ShouldNotBeNull();
            channelSummariesRetrieved.Count.ShouldEqual(1);
            channelSummariesRetrieved[0].ShouldNotBeNull();
            channelSummariesRetrieved[0].ShouldEqual(channelDto);
        }

        [Test]
        public void CanCreateFormViewModel() {
            // Establish Context
            ChannelFormViewModel viewModelToExpect = new ChannelFormViewModel();

            // Act
            ChannelFormViewModel viewModelRetrieved =
                channelManagementService.CreateFormViewModel();

            // Assert
            viewModelRetrieved.ShouldNotBeNull();
            viewModelRetrieved.Channel.ShouldBeNull();
        }

        [Test]
        public void CanCreateFormViewModelForchannel() {
            // Establish Context
            ChannelFormViewModel viewModelToExpect = new ChannelFormViewModel();

            Channel channel = 
                ChannelInstanceFactory.CreateValidTransientChannel();

            channelRepository.Expect(r => r.Get(1))
                .Return(channel);

            // Act
            ChannelFormViewModel viewModelRetrieved =
                channelManagementService.CreateFormViewModelFor(1);

            // Assert
            viewModelRetrieved.ShouldNotBeNull();
            viewModelRetrieved.Channel.ShouldNotBeNull();
            viewModelRetrieved.Channel.ShouldEqual(channel);
        }

        [Test]
        public void CanSaveOrUpdateValidchannel() {
            // Establish Context
            Channel validchannel = 
                ChannelInstanceFactory.CreateValidTransientChannel();

            // Act
            ActionConfirmation confirmation =
                channelManagementService.SaveOrUpdate(validchannel);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldNotBeNull();
            confirmation.Value.ShouldEqual(validchannel);
        }

        [Test]
        public void CannotSaveOrUpdateInvalidchannel() {
            // Establish Context
            Channel invalidchannel = new Channel();

            // Act
            ActionConfirmation confirmation =
                channelManagementService.SaveOrUpdate(invalidchannel);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CanUpdateWithValidchannelFromForm() {
            // Establish Context
            Channel validchannelFromForm = 
                ChannelInstanceFactory.CreateValidTransientChannel();
            
            // Intentionally empty to ensure successful transfer of values
            Channel channelFromDb = new Channel();

            channelRepository.Expect(r => r.Get(1))
                .Return(channelFromDb);

            // Act
            ActionConfirmation confirmation =
                channelManagementService.UpdateWith(validchannelFromForm, 1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldNotBeNull();
            confirmation.Value.ShouldEqual(channelFromDb);
            confirmation.Value.ShouldEqual(validchannelFromForm);
        }

        [Test]
        public void CannotUpdateWithInvalidchannelFromForm() {
            // Establish Context
            Channel invalidchannelFromForm = new Channel();

            // Intentionally empty to ensure successful transfer of values
            Channel channelFromDb = new Channel();

            channelRepository.Expect(r => r.Get(1))
                .Return(channelFromDb);

            // Act
            ActionConfirmation confirmation =
                channelManagementService.UpdateWith(invalidchannelFromForm, 1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CanDeletechannel() {
            // Establish Context
            Channel channelToDelete = new Channel();

            channelRepository.Expect(r => r.Get(1))
                .Return(channelToDelete);

            // Act
            ActionConfirmation confirmation =
                channelManagementService.Delete(1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CannotDeleteNonexistentchannel() {
            // Establish Context
            channelRepository.Expect(r => r.Get(1))
                .Return(null);

            // Act
            ActionConfirmation confirmation =
                channelManagementService.Delete(1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        private IChannelRepository channelRepository;
        private IChannelManagementService channelManagementService;
    }
}
