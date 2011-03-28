using System.Collections.Generic;
using System;
using System.Drawing;
using System.IO;
using ImageExtraction;
using Oxigen.ApplicationServices.Flash;
using SharpArch.Core;
using Oxigen.Core;
using Oxigen.ApplicationServices.ViewModels;
using Oxigen.Core.QueryDtos;
using Oxigen.Core.RepositoryInterfaces;
 

namespace Oxigen.ApplicationServices
{
    public class SlideManagementService : ISlideManagementService
    {
        public SlideManagementService(ISlideRepository slideRepository, IAssetContentRepository assetContentRepository) {
            Check.Require(slideRepository != null, "slideRepository may not be null");
            Check.Require(assetContentRepository != null, "assetContentRepository may not be null");

            this.slideRepository = slideRepository;
            this.assetContentRepository = assetContentRepository;
        }

        public Slide Get(int id) {
            return slideRepository.Get(id);
        }

        public IList<Slide> GetAll() {
            return slideRepository.GetAll();
        }

        public IList<SlideDto> GetSlideSummaries() {
            return slideRepository.GetSlideSummaries();
        }

        public SlideFormViewModel CreateFormViewModel() {
            SlideFormViewModel viewModel = new SlideFormViewModel();
            return viewModel;
        }

        public SlideFormViewModel CreateFormViewModelFor(int slideId) {
            Slide slide = slideRepository.Get(slideId);
            return CreateFormViewModelFor(slide);
        }

        public SlideFormViewModel CreateFormViewModelFor(Slide slide) {
            SlideFormViewModel viewModel = CreateFormViewModel();
            viewModel.Slide = slide;
            return viewModel;
        }

        public ActionConfirmation SaveOrUpdate(Slide slide) {
            if (slide.IsValid()) {
                slideRepository.SaveOrUpdate(slide);

                ActionConfirmation saveOrUpdateConfirmation = ActionConfirmation.CreateSuccessConfirmation(
                    "The slide was successfully saved.");
                saveOrUpdateConfirmation.Value = slide;

                return saveOrUpdateConfirmation;
            }
            else {
                slideRepository.DbContext.RollbackTransaction();

                return ActionConfirmation.CreateFailureConfirmation(
                    "The slide could not be saved due to missing or invalid information.");
            }
        }

        public ActionConfirmation UpdateWith(Slide slideFromForm, int idOfSlideToUpdate) {
            Slide slideToUpdate = 
                slideRepository.Get(idOfSlideToUpdate);
            TransferFormValuesTo(slideToUpdate, slideFromForm);

            if (slideToUpdate.IsValid()) {
                ActionConfirmation updateConfirmation = ActionConfirmation.CreateSuccessConfirmation(
                    "The slide was successfully updated.");
                updateConfirmation.Value = slideToUpdate;

                return updateConfirmation;
            }
            else {
                slideRepository.DbContext.RollbackTransaction();

                return ActionConfirmation.CreateFailureConfirmation(
                    "The slide could not be saved due to missing or invalid information.");
            }
        }

        public ActionConfirmation Delete(int id) {
            Slide slideToDelete = slideRepository.Get(id);

            if (slideToDelete != null) {
                slideRepository.Delete(slideToDelete);

                try {
                    slideRepository.DbContext.CommitChanges();
                    
                    return ActionConfirmation.CreateSuccessConfirmation(
                        "The slide was successfully deleted.");
                }
                catch {
                    slideRepository.DbContext.RollbackTransaction();

                    return ActionConfirmation.CreateFailureConfirmation(
                        "A problem was encountered preventing the slide from being deleted. " +
                        "Another item likely depends on this slide.");
                }
            }
            else {
                return ActionConfirmation.CreateFailureConfirmation(
                    "The slide could not be found for deletion. It may already have been deleted.");
            }
        }



      public ActionConfirmation CreateFromTemplate(int userId, int slidefolderId, IList<int> assetContentIds, int templateId, string caption, string credit)
        {
            string templatePath = (string)System.Configuration.ConfigurationSettings.AppSettings["templatePath"];

            //TODO Ensure user owns slidefolder

            var bUseTemplate = (templateId != 0);

            foreach (var assetContentId in assetContentIds)
            {
                var assetContent = this.assetContentRepository.Get(assetContentId);
                var extension = bUseTemplate ? ".swf" : assetContent.FilenameExtension;
                var slide = new Slide(extension);

                slide.SlideFolderID = slidefolderId;
                slide.Caption = assetContent.Caption;
                slide.ClickThroughURL = assetContent.URL;
                slide.Creator = assetContent.Creator;
                slide.DisplayDuration = assetContent.DisplayDuration;
                slide.UserGivenDate = assetContent.UserGivenDate;
                slide.Name = assetContent.Name;
                slide.Length = assetContent.Length;

                if (bUseTemplate)
                {
                    slide.PreviewType = "Flash";
                    slide.PlayerType = "Flash";

                    var template = new SWAFile(templatePath + "Arsenal.swf");
                    template.UpdateBitmap("MasterImage", Image.FromFile(assetContent.FileFullPathName));
                    template.UpdateText("MasterText", caption);
                    template.UpdateText("CreditText", credit);
                    var image = template.GetThumbnail();
                    ImageUtilities.Crop(image, 100, 75, AnchorPosition.Center).Save(slide.ThumbnailFullPathName);
                    template.Save(slide.FileFullPathName);
                }
                else
                {
                    slide.PreviewType = assetContent.PreviewType;
                    if (slide.PreviewType == "Image")
                        slide.PlayerType = "Image";
                    else if (slide.PreviewType == "Flash")
                        slide.PlayerType = "Flash";
                    else if (slide.FilenameExtension == ".mov")
                        slide.PlayerType = "VideoQT";
                    else
                        slide.PlayerType = "VideoNonQT";

                    File.Copy(assetContent.FileFullPathName, slide.FileFullPathName);
                    File.Copy(assetContent.ThumbnailFullPathName, slide.ThumbnailFullPathName);

                    //TODO: ExtractThumbnails(slideContents)
                    //ExtractThumbnails(slideContents);

                }

                slideRepository.SaveOrUpdate(slide);
            }
       
            return ActionConfirmation.CreateSuccessConfirmation("Slide successfully create");
        }

        private void TransferFormValuesTo(Slide slideToUpdate, Slide slideFromForm) {
		    slideToUpdate.Filename = slideFromForm.Filename;
			slideToUpdate.FilenameExtension = slideFromForm.FilenameExtension;
			slideToUpdate.FilenameNoPath = slideFromForm.FilenameNoPath;
			slideToUpdate.GUID = slideFromForm.GUID;
			slideToUpdate.SubDir = slideFromForm.SubDir;
			slideToUpdate.Name = slideFromForm.Name;
			slideToUpdate.Creator = slideFromForm.Creator;
			slideToUpdate.Caption = slideFromForm.Caption;
			slideToUpdate.ClickThroughURL = slideFromForm.ClickThroughURL;
			slideToUpdate.WebsiteURL = slideFromForm.WebsiteURL;
			slideToUpdate.DisplayDuration = slideFromForm.DisplayDuration;
			slideToUpdate.Length = slideFromForm.Length;
			slideToUpdate.ImagePath = slideFromForm.ImagePath;
			slideToUpdate.ImagePathWinFS = slideFromForm.ImagePathWinFS;
			slideToUpdate.ImageName = slideFromForm.ImageName;
			slideToUpdate.PlayerType = slideFromForm.PlayerType;
			slideToUpdate.PreviewType = slideFromForm.PreviewType;
			slideToUpdate.bLocked = slideFromForm.bLocked;
			slideToUpdate.UserGivenDate = slideFromForm.UserGivenDate;
			slideToUpdate.AddDate = slideFromForm.AddDate;
			slideToUpdate.EditDate = slideFromForm.EditDate;
			slideToUpdate.MadeDirtyLastDate = slideFromForm.MadeDirtyLastDate;
        }

        ISlideRepository slideRepository;
        IAssetContentRepository assetContentRepository;
    }
}
