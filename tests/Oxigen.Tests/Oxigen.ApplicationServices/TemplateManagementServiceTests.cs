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
    public class TemplateManagementServiceTests
    {
        [SetUp]
        public void SetUp() {
            ServiceLocatorInitializer.Init();

            templateRepository = 
                MockRepository.GenerateMock<ITemplateRepository>();
            templateRepository.Stub(r => r.DbContext)
                .Return(MockRepository.GenerateMock<IDbContext>());
            
            templateManagementService =
                new TemplateManagementService(templateRepository);
        }

        [Test]
        public void CanGetTemplate() {
            // Establish Context
            Template templateToExpect = 
                TemplateInstanceFactory.CreateValidTransientTemplate();

            templateRepository.Expect(r => r.Get(1))
                .Return(templateToExpect);

            // Act
            Template templateRetrieved = 
                templateManagementService.Get(1);

            // Assert
            templateRetrieved.ShouldNotBeNull();
            templateRetrieved.ShouldEqual(templateToExpect);
        }

        [Test]
        public void CanGetAllTemplates() {
            // Establish Context
            IList<Template> templatesToExpect = new List<Template>();

            Template template = 
                TemplateInstanceFactory.CreateValidTransientTemplate();

            templatesToExpect.Add(template);

            templateRepository.Expect(r => r.GetAll())
                .Return(templatesToExpect);

            // Act
            IList<Template> templatesRetrieved =
                templateManagementService.GetAll();

            // Assert
            templatesRetrieved.ShouldNotBeNull();
            templatesRetrieved.Count.ShouldEqual(1);
            templatesRetrieved[0].ShouldNotBeNull();
            templatesRetrieved[0].ShouldEqual(template);
        }

        [Test]
        public void CanGetTemplateSummaries() {
            // Establish Context
            IList<TemplateDto> templateSummariesToExpect = new List<TemplateDto>();

            TemplateDto templateDto = new TemplateDto();
            templateSummariesToExpect.Add(templateDto);

            templateRepository.Expect(r => r.GetTemplateSummaries())
                .Return(templateSummariesToExpect);

            // Act
            IList<TemplateDto> templateSummariesRetrieved =
                templateManagementService.GetTemplateSummaries();

            // Assert
            templateSummariesRetrieved.ShouldNotBeNull();
            templateSummariesRetrieved.Count.ShouldEqual(1);
            templateSummariesRetrieved[0].ShouldNotBeNull();
            templateSummariesRetrieved[0].ShouldEqual(templateDto);
        }

        [Test]
        public void CanCreateFormViewModel() {
            // Establish Context
            TemplateFormViewModel viewModelToExpect = new TemplateFormViewModel();

            // Act
            TemplateFormViewModel viewModelRetrieved =
                templateManagementService.CreateFormViewModel();

            // Assert
            viewModelRetrieved.ShouldNotBeNull();
            viewModelRetrieved.Template.ShouldBeNull();
        }

        [Test]
        public void CanCreateFormViewModelForTemplate() {
            // Establish Context
            TemplateFormViewModel viewModelToExpect = new TemplateFormViewModel();

            Template template = 
                TemplateInstanceFactory.CreateValidTransientTemplate();

            templateRepository.Expect(r => r.Get(1))
                .Return(template);

            // Act
            TemplateFormViewModel viewModelRetrieved =
                templateManagementService.CreateFormViewModelFor(1);

            // Assert
            viewModelRetrieved.ShouldNotBeNull();
            viewModelRetrieved.Template.ShouldNotBeNull();
            viewModelRetrieved.Template.ShouldEqual(template);
        }

        [Test]
        public void CanSaveOrUpdateValidTemplate() {
            // Establish Context
            Template validTemplate = 
                TemplateInstanceFactory.CreateValidTransientTemplate();

            // Act
            ActionConfirmation confirmation =
                templateManagementService.SaveOrUpdate(validTemplate);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldNotBeNull();
            confirmation.Value.ShouldEqual(validTemplate);
        }

        [Test]
        public void CannotSaveOrUpdateInvalidTemplate() {
            // Establish Context
            Template invalidTemplate = new Template();

            // Act
            ActionConfirmation confirmation =
                templateManagementService.SaveOrUpdate(invalidTemplate);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CanUpdateWithValidTemplateFromForm() {
            // Establish Context
            Template validTemplateFromForm = 
                TemplateInstanceFactory.CreateValidTransientTemplate();
            
            // Intentionally empty to ensure successful transfer of values
            Template templateFromDb = new Template();

            templateRepository.Expect(r => r.Get(1))
                .Return(templateFromDb);

            // Act
            ActionConfirmation confirmation =
                templateManagementService.UpdateWith(validTemplateFromForm, 1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldNotBeNull();
            confirmation.Value.ShouldEqual(templateFromDb);
            confirmation.Value.ShouldEqual(validTemplateFromForm);
        }

        [Test]
        public void CannotUpdateWithInvalidTemplateFromForm() {
            // Establish Context
            Template invalidTemplateFromForm = new Template();

            // Intentionally empty to ensure successful transfer of values
            Template templateFromDb = new Template();

            templateRepository.Expect(r => r.Get(1))
                .Return(templateFromDb);

            // Act
            ActionConfirmation confirmation =
                templateManagementService.UpdateWith(invalidTemplateFromForm, 1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CanDeleteTemplate() {
            // Establish Context
            Template templateToDelete = new Template();

            templateRepository.Expect(r => r.Get(1))
                .Return(templateToDelete);

            // Act
            ActionConfirmation confirmation =
                templateManagementService.Delete(1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CannotDeleteNonexistentTemplate() {
            // Establish Context
            templateRepository.Expect(r => r.Get(1))
                .Return(null);

            // Act
            ActionConfirmation confirmation =
                templateManagementService.Delete(1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        private ITemplateRepository templateRepository;
        private ITemplateManagementService templateManagementService;
    }
}
