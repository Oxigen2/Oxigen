using System.Collections.Generic;
using Oxigen.Core;
using Oxigen.ApplicationServices.ViewModels;
using Oxigen.Core.QueryDtos;
 

namespace Oxigen.ApplicationServices
{
    public interface IChannelManagementService
    {
        ChannelFormViewModel CreateFormViewModel();
        ChannelFormViewModel CreateFormViewModelFor(int channelId);
        ChannelFormViewModel CreateFormViewModelFor(Channel channel);
        Channel Get(int id);
        IList<Channel> GetAll();
        IList<ChannelDto> GetChannelSummaries();
        ActionConfirmation SaveOrUpdate(Channel channel);
        ActionConfirmation UpdateWith(Channel channelFromForm, int idOfChannelToUpdate);
        ActionConfirmation Delete(int id);
    }
}
