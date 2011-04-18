using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Oxigen.Core;

namespace Oxigen.Data.NHibernateMaps
{
    public class SlideFolderMap : IAutoMappingOverride<SlideFolder>
    {
        public void Override(AutoMapping<SlideFolder> mapping) {
            mapping.Map(x => x.SlideCount).Access.ReadOnly();
        }
    }
}