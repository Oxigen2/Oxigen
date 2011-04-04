using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace Oxigen.Data.NHibernateMaps.Conventions
{
    public class PrimaryKeyConvention : IIdConvention
    {
        public void Apply(FluentNHibernate.Conventions.Instances.IIdentityInstance instance)
        {
            instance.Column(instance.EntityType.Name + "_ID");
            instance.UnsavedValue("0");
            instance.GeneratedBy.Identity();
        }
    }
}
