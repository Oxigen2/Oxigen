using System;

namespace Oxigen.Core.QueryDtos
{
    public class AssetContentDto
    {
        public int ASSETCONTENT_ID { get; set; }
        public int Id { get { return ASSETCONTENT_ID; } set { ASSETCONTENT_ID = value; } }
		public string Name { get; set; }
		public string Caption { get; set; }
		public string GUID { get; set; }
    }
}
