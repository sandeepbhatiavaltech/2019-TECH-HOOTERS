using Glass.Mapper.Sc.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.Boilerplate.Feature.Navigation.Models.Glassmapper
{
    public class IHomeMap : SitecoreGlassMap<IHomeModel>
    {
        public override void Configure()
        {
            Map(config =>
            {
                config.AutoMap();
                config.Id(f => f.Id);
                config.Field(f => f.Title).FieldName("Title");
                config.Field(f => f.Content).FieldName("Content");
            });
        }
    }
}