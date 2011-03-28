using System.Collections.Generic;
using Oxigen.Core;
using Oxigen.ApplicationServices.ViewModels;
using Oxigen.Core.QueryDtos;
 

namespace Oxigen.ApplicationServices
{
    public interface IAssetContentManagementService
    {
        AssetContentFormViewModel CreateFormViewModel();
        AssetContentFormViewModel CreateFormViewModelFor(int assetContentId);
        AssetContentFormViewModel CreateFormViewModelFor(AssetContent assetContent);
        AssetContent Get(int id);
        IList<AssetContent> GetAll();
        IList<AssetContentDto> GetAssetContentSummaries();
        ActionConfirmation SaveOrUpdate(AssetContent assetContent);
        ActionConfirmation UpdateWith(AssetContent assetContentFromForm, int idOfAssetContentToUpdate);
        ActionConfirmation Delete(int id);
    }
}
