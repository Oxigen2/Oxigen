using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Oxigen.Core;

namespace Oxigen.Data.NHibernateMaps
{
    public class PublisherMap : IAutoMappingOverride<Publisher>
    {
        public void Override(AutoMapping<Publisher> mapping)
        {
            mapping.Id(x => x.Id, "PUBLISHER_ID")
                .UnsavedValue(0)
                .GeneratedBy.Identity();
            mapping.HasMany(x => x.AssignedTemplates);


        }
    }
}