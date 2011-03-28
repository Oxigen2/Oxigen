using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Oxigen.Core;

namespace Oxigen.Data.NHibernateMaps
{
    public class SlideMap : IAutoMappingOverride<Slide>
    {
        public void Override(AutoMapping<Slide> mapping)
        {
            mapping.Id(x => x.Id, "SLIDE_ID")
                .UnsavedValue(0)
                .GeneratedBy.Identity();
            mapping.Map(x => x.Name).Column("SlideName");
            mapping.Map(x => x.ImageName).Column("ImageFilename");
            mapping.IgnoreProperty(x => x.FileFullPathName);
            mapping.IgnoreProperty(x => x.ThumbnailFullPathName);


        }
    }
}