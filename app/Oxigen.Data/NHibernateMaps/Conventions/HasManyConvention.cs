﻿using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace Oxigen.Data.NHibernateMaps.Conventions
{
    public class HasManyConvention : IHasManyConvention
    {
        public void Apply(FluentNHibernate.Conventions.Instances.IOneToManyCollectionInstance instance)
        {
            instance.Key.Column(instance.EntityType.Name + "ID");
            instance.Inverse();
            instance.Cascade.All();
        }
    }
}
