using System;
using FluentNHibernate;

namespace Oxigen.Data.NHibernateMaps.Conventions
{
    public class ForeignKeyConvention : FluentNHibernate.Conventions.ForeignKeyConvention
    {
        protected override string GetKeyName(Member property, Type type)
        {
            if (property == null)
                return type.Name + "ID";

            return property.Name + "ID";
        }
    }
}
