using System.Collections.Generic;
using Oxigen.Core;
using Oxigen.ApplicationServices.ViewModels;
using Oxigen.Core.QueryDtos;
 

namespace Oxigen.ApplicationServices
{
    public interface IChannelsSlideManagementService
    {
        ChannelsSlideFormViewModel CreateFormViewModel();
        ChannelsSlideFormViewModel CreateFormViewModelFor(int channelsSlideId);
        ChannelsSlideFormViewModel CreateFormViewModelFor(ChannelsSlide channelsSlide);
        ChannelsSlide Get(int id);
        IList<ChannelsSlide> GetAll();
        IList<ChannelsSlideDto> GetChannelsSlideSummaries();
        ActionConfirmation SaveOrUpdate(ChannelsSlide channelsSlide);
        ActionConfirmation UpdateWith(ChannelsSlide channelsSlideFromForm, int idOfChannelsSlideToUpdate);
        ActionConfirmation Delete(int id);
    }
}
