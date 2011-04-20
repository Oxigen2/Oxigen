using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Oxigen.Core;

namespace Oxigen.Data.NHibernateMaps
{
    public class ChannelMap : IAutoMappingOverride<Channel>
    {
        public void Override(AutoMapping<Channel> mapping)
        {
            mapping.Map(x => x.NoContent).Not.Update();
            mapping.Map(x => x.NoFollowers).Not.Update();
        }
    }
}