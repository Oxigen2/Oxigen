using System;
using Oxigen.Core;

namespace Tests.Oxigen.Core
{
    public class TemplateInstanceFactory
    {
        public static Template CreateValidTransientTemplate() {
            return new Template() {
			    MetaData = "MetaData" 
            };
        }
    }
}
