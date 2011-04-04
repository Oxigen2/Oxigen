using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Oxigen.Core;

namespace Oxigen.Data.NHibernateMaps
{
    public class TemplateMap : IAutoMappingOverride<Template>
    {
        public void Override(AutoMapping<Template> mapping) {
            //mapping.References(x => x.Publisher);
            mapping.IgnoreProperty(x => x.FileFullPathName);
            mapping.IgnoreProperty(x => x.ThumbnailFullPathName);

        }
    }
}