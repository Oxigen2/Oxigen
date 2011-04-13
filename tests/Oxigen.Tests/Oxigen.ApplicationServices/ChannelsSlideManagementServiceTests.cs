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
    public class ChannelsSlideManagementServiceTests
    {
        [SetUp]
        public void SetUp() {
            ServiceLocatorInitializer.Init();

            channelsSlideRepository = 
                MockRepository.GenerateMock<IChannelsSlideRepository>();
            channelsSlideRepository.Stub(r => r.DbContext)
                .Return(MockRepository.GenerateMock<IDbContext>());
            
            channelsSlideManagementService =
                new ChannelsSlideManagementService(channelsSlideRepository);
        }

        [Test]
        public void CanGetChannelsSlide() {
            // Establish Context
            ChannelsSlide channelsSlideToExpect = 
                ChannelsSlideInstanceFactory.CreateValidTransientChannelsSlide();

            channelsSlideRepository.Expect(r => r.Get(1))
                .Return(channelsSlideToExpect);

            // Act
            ChannelsSlide channelsSlideRetrieved = 
                channelsSlideManagementService.Get(1);

            // Assert
            channelsSlideRetrieved.ShouldNotBeNull();
            channelsSlideRetrieved.ShouldEqual(channelsSlideToExpect);
        }

        [Test]
        public void CanGetAllChannelsSlides() {
            // Establish Context
            IList<ChannelsSlide> channelsSlidesToExpect = new List<ChannelsSlide>();

            ChannelsSlide channelsSlide = 
                ChannelsSlideInstanceFactory.CreateValidTransientChannelsSlide();

            channelsSlidesToExpect.Add(channelsSlide);

            channelsSlideRepository.Expect(r => r.GetAll())
                .Return(channelsSlidesToExpect);

            // Act
            IList<ChannelsSlide> channelsSlidesRetrieved =
                channelsSlideManagementService.GetAll();

            // Assert
            channelsSlidesRetrieved.ShouldNotBeNull();
            channelsSlidesRetrieved.Count.ShouldEqual(1);
            channelsSlidesRetrieved[0].ShouldNotBeNull();
            channelsSlidesRetrieved[0].ShouldEqual(channelsSlide);
        }

        [Test]
        public void CanGetChannelsSlideSummaries() {
            // Establish Context
            IList<ChannelsSlideDto> channelsSlideSummariesToExpect = new List<ChannelsSlideDto>();

            ChannelsSlideDto channelsSlideDto = new ChannelsSlideDto();
            channelsSlideSummariesToExpect.Add(channelsSlideDto);

            channelsSlideRepository.Expect(r => r.GetChannelsSlideSummaries())
                .Return(channelsSlideSummariesToExpect);

            // Act
            IList<ChannelsSlideDto> channelsSlideSummariesRetrieved =
                channelsSlideManagementService.GetChannelsSlideSummaries();

            // Assert
            channelsSlideSummariesRetrieved.ShouldNotBeNull();
            channelsSlideSummariesRetrieved.Count.ShouldEqual(1);
            channelsSlideSummariesRetrieved[0].ShouldNotBeNull();
            channelsSlideSummariesRetrieved[0].ShouldEqual(channelsSlideDto);
        }

        [Test]
        public void CanCreateFormViewModel() {
            // Establish Context
            ChannelsSlideFormViewModel viewModelToExpect = new ChannelsSlideFormViewModel();

            // Act
            ChannelsSlideFormViewModel viewModelRetrieved =
                channelsSlideManagementService.CreateFormViewModel();

            // Assert
            viewModelRetrieved.ShouldNotBeNull();
            viewModelRetrieved.ChannelsSlide.ShouldBeNull();
        }

        [Test]
        public void CanCreateFormViewModelForChannelsSlide() {
            // Establish Context
            ChannelsSlideFormViewModel viewModelToExpect = new ChannelsSlideFormViewModel();

            ChannelsSlide channelsSlide = 
                ChannelsSlideInstanceFactory.CreateValidTransientChannelsSlide();

            channelsSlideRepository.Expect(r => r.Get(1))
                .Return(channelsSlide);

            // Act
            ChannelsSlideFormViewModel viewModelRetrieved =
                channelsSlideManagementService.CreateFormViewModelFor(1);

            // Assert
            viewModelRetrieved.ShouldNotBeNull();
            viewModelRetrieved.ChannelsSlide.ShouldNotBeNull();
            viewModelRetrieved.ChannelsSlide.ShouldEqual(channelsSlide);
        }

        [Test]
        public void CanSaveOrUpdateValidChannelsSlide() {
            // Establish Context
            ChannelsSlide validChannelsSlide = 
                ChannelsSlideInstanceFactory.CreateValidTransientChannelsSlide();

            // Act
            ActionConfirmation confirmation =
                channelsSlideManagementService.SaveOrUpdate(validChannelsSlide);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldNotBeNull();
            confirmation.Value.ShouldEqual(validChannelsSlide);
        }

        [Test]
        public void CannotSaveOrUpdateInvalidChannelsSlide() {
            // Establish Context
            ChannelsSlide invalidChannelsSlide = new ChannelsSlide();

            // Act
            ActionConfirmation confirmation =
                channelsSlideManagementService.SaveOrUpdate(invalidChannelsSlide);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CanUpdateWithValidChannelsSlideFromForm() {
            // Establish Context
            ChannelsSlide validChannelsSlideFromForm = 
                ChannelsSlideInstanceFactory.CreateValidTransientChannelsSlide();
            
            // Intentionally empty to ensure successful transfer of values
            ChannelsSlide channelsSlideFromDb = new ChannelsSlide();

            channelsSlideRepository.Expect(r => r.Get(1))
                .Return(channelsSlideFromDb);

            // Act
            ActionConfirmation confirmation =
                channelsSlideManagementService.UpdateWith(validChannelsSlideFromForm, 1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldNotBeNull();
            confirmation.Value.ShouldEqual(channelsSlideFromDb);
            confirmation.Value.ShouldEqual(validChannelsSlideFromForm);
        }

        [Test]
        public void CannotUpdateWithInvalidChannelsSlideFromForm() {
            // Establish Context
            ChannelsSlide invalidChannelsSlideFromForm = new ChannelsSlide();

            // Intentionally empty to ensure successful transfer of values
            ChannelsSlide channelsSlideFromDb = new ChannelsSlide();

            channelsSlideRepository.Expect(r => r.Get(1))
                .Return(channelsSlideFromDb);

            // Act
            ActionConfirmation confirmation =
                channelsSlideManagementService.UpdateWith(invalidChannelsSlideFromForm, 1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CanDeleteChannelsSlide() {
            // Establish Context
            ChannelsSlide channelsSlideToDelete = new ChannelsSlide();

            channelsSlideRepository.Expect(r => r.Get(1))
                .Return(channelsSlideToDelete);

            // Act
            ActionConfirmation confirmation =
                channelsSlideManagementService.Delete(1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CannotDeleteNonexistentChannelsSlide() {
            // Establish Context
            channelsSlideRepository.Expect(r => r.Get(1))
                .Return(null);

            // Act
            ActionConfirmation confirmation =
                channelsSlideManagementService.Delete(1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        private IChannelsSlideRepository channelsSlideRepository;
        private IChannelsSlideManagementService channelsSlideManagementService;
    }
}
