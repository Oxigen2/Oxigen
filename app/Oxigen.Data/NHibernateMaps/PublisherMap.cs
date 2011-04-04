using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Oxigen.Core;

namespace Oxigen.Data.NHibernateMaps
{
    public class PublisherMap : IAutoMappingOverride<Publisher>
    {
        public void Override(AutoMapping<Publisher> mapping)
        {
            mapping.HasMany(x => x.AssignedTemplates);


        }
    }
}