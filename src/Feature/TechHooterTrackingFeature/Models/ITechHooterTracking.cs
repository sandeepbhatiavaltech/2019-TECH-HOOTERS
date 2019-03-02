using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace TechHooterTrackingFeature.Models
{
    public class ITechHooterTracking
    {
        public virual Guid Id { get; }
        bool IsEnabled { get; }
        string ApiUrl { get; }
        string ApiKey { get; }
        string Format { get; }

        [SitecoreField(Name_Value_ListFieldName)]
        public virtual string Name_Value_List_Raw { get; set; }

        public string Name_Value_List { get { return HttpUtility.ParseQueryString(Name_Value_List_Raw); } }

    }
    }
}