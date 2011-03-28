using System;
using System.Linq;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using Oxigen.Core;
using Oxigen.Data.NHibernateMaps.Conventions;
using SharpArch.Core.DomainModel;
using SharpArch.Data.NHibernate.FluentNHibernate;

namespace Oxigen.Data.NHibernateMaps
{

    public class AutoPersistenceModelGenerator : IAutoPersistenceModelGenerator
    {

        #region IAutoPersistenceModelGenerator Members

        public AutoPersistenceModel Generate()
        {
            return AutoMap.AssemblyOf<Class1>(new AutomappingConfiguration())
                .Conventions.Setup(GetConventions())
                .IgnoreBase<Entity>()
                .IgnoreBase<AssetFile>()
                .IgnoreBase(typeof(EntityWithTypedId<>))
                .UseOverridesFromAssemblyOf<AutoPersistenceModelGenerator>();
        }

        #endregion

        private Action<IConventionFinder> GetConventions()
        {
            return c =>
            {
                c.Add<Oxigen.Data.NHibernateMaps.Conventions.ForeignKeyConvention>();
                c.Add<Oxigen.Data.NHibernateMaps.Conventions.HasManyConvention>();
                c.Add<Oxigen.Data.NHibernateMaps.Conventions.HasManyToManyConvention>();
                c.Add<Oxigen.Data.NHibernateMaps.Conventions.ManyToManyTableNameConvention>();
                c.Add<Oxigen.Data.NHibernateMaps.Conventions.PrimaryKeyConvention>();
                c.Add<Oxigen.Data.NHibernateMaps.Conventions.ReferenceConvention>();
                c.Add<Oxigen.Data.NHibernateMaps.Conventions.TableNameConvention>();
            };
        }
    }
}
