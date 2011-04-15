using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Oxigen.Core;

namespace Oxigen.Data.NHibernateMaps
{
    public class SlideMap : IAutoMappingOverride<Slide>
    {
        public void Override(AutoMapping<Slide> mapping)
        {
            mapping.Map(x => x.Name).Column("SlideName");
            mapping.Map(x => x.ImageName).Column("ImageFilename");
            mapping.IgnoreProperty(x => x.FileFullPathName);
            mapping.IgnoreProperty(x => x.ThumbnailFullPathName);
            mapping.HasMany(x => x.AssignedChannels)
                .Cascade.All()
                .Inverse();


        }
    }
}