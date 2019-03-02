using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechHooterTrackingFeature.Models
{
    [SitecoreType(TemplateId = "{00003E08-735B-4509-90D3-E5C9316878B2}", AutoMap = true)]
    public class TechHooterTracking
    {
        public virtual Guid Id { get; }
        [SitecoreField("IsEnabled")]
        public virtual bool IsEnabled { get; }
        [SitecoreField("ApiUrl")]
        public virtual string ApiUrl { get; }
        [SitecoreField("ApiKey")]
        public virtual string ApiKey { get; }
        [SitecoreField("Format")]
        public virtual string Format { get; }

        [SitecoreField("ParametersDetails")]
        public virtual string Name_Value_List_Raw { get; set; }

        public string Name_Value_List
        {
            get
            {
                return Convert.ToString(HttpUtility.ParseQueryString(Name_Value_List_Raw));
            }
        }
    }
}