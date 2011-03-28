using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Oxigen.Core;

namespace Oxigen.Data.NHibernateMaps
{
    public class AssetContentMap : IAutoMappingOverride<AssetContent>
    {
        public void Override(AutoMapping<AssetContent> mapping)
        {
            mapping.Id(x => x.Id, "ASSETCONTENT_ID")
                .UnsavedValue(0)
                .GeneratedBy.Identity();
            mapping.IgnoreProperty(x => x.FileFullPathName);
            mapping.IgnoreProperty(x => x.ThumbnailFullPathName);
        
        }
    }
}
