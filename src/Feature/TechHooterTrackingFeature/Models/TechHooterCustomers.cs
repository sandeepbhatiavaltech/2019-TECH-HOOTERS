using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechHooterTrackingFeature.Models
{
    public class TechHooterCustomers
    {
        public Guid ContactId { get; set; }
        public string CustomerName { get; set; }
        public DateTime AccessDate { get; set; }
        public string IPAddress { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string CityName { get; set; }
        // public string RegionName { get; set; }
        public string ZipCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        // public string TimeZone { get; set; }
        public string Place { get; set; }
        public string State { get; set; }
        public string Event { get; set; }
        public string Email { get; set; }
    }
}