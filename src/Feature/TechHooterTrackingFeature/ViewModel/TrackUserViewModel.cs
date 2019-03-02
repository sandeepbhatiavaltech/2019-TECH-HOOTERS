using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechHooterTrackingFeature.Models;

namespace TechHooterTrackingFeature.ViewModel
{
    public class TrackUserViewModel
    {
        public TechHooterTracking TrackCustomer { get; set; }

        public List<TechHooterCustomers> Customers = new List<TechHooterCustomers>();

    }
}