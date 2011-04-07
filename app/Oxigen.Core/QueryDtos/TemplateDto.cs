using System;

namespace Oxigen.Core.QueryDtos
{
    public class TemplateDto
    {
        public int TEMPLATE_ID { get; set; }
        public int Id { get { return TEMPLATE_ID; } set { TEMPLATE_ID = value; } }
		public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
