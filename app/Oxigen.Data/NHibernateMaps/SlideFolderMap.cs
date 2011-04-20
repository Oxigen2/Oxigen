using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Oxigen.Core;

namespace Oxigen.Data.NHibernateMaps
{
    public class SlideFolderMap : IAutoMappingOverride<SlideFolder>
    {
        public void Override(AutoMapping<SlideFolder> mapping) {
            mapping.Map(x => x.SlideCount).Not.Update();
            mapping.HasMany(x => x.Slides).OrderBy("SLIDE_ID");
        }
    }
}