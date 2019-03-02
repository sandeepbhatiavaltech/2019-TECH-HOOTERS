using EmailUtility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using TechHooterTrackingFeature.Models;



namespace TechHooterTrackingFeature.Repositories
{
    public class TechHooterTrackingRepositories
    {
        /// <summary>
        /// Retriving the IP address from Client Browser and Location Via using Via
        /// </summary>
        /// <param name="model">Pass the Client Device Details in the Model</param>
        /// <param name="latitude">{ass</param>
        /// <param name="longitude"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public List<TechHooterCustomers> SaveEventAction(TechHooterTracking model,string latitude,string longitude, string action)
        {
            string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            
            List<TechHooterCustomers> locations = new List<TechHooterCustomers>();
            TechHooterCustomers _customerinfo = new TechHooterCustomers();

            string JsonData = GetCallApi(model,latitude, longitude);
           
            var parsed = JObject.Parse(JsonData);
            var results = parsed.SelectToken("results").Children();
            foreach (var innerlocation in results)
            {
                var customerlocation = innerlocation.SelectToken("formatted");
                _customerinfo.ContactId = Guid.NewGuid();
                _customerinfo.CustomerName = "jitender";
                _customerinfo.AccessDate = System.DateTime.Now;
                _customerinfo.Place = ((JValue)customerlocation).Value.ToString();
                _customerinfo.IPAddress = ipAddress;
                _customerinfo.Event = action;
                var customerdetails = innerlocation.SelectToken("components");
                JObject innerinfo = JObject.Parse(customerdetails.ToString());
                var receivingparams = Regex.Matches(model.Name_Value_List, "([^?=&]+)(=([^&]*))?").Cast<Match>().ToDictionary(x => x.Groups[1].Value, x => x.Groups[3].Value);
                foreach (var info in innerinfo)
                {
                    //string receivingkey = "";
                    //bool required = IsRequiredField(receivingparams, info.Key.ToLower(),ref receivingkey);
                    //if (info.Key.ToLower() == receivingkey && required)
                    //{
                    //    _customerinfo.CountryName = info.Value.ToString();
                    //}
                    //else if (info.Key.ToLower() == receivingkey && required)
                    //{
                    //    _customerinfo.CountryCode = info.Value.ToString();
                    //}
                    //else if (info.Key.ToLower() == receivingkey && required)
                    //{
                    //    _customerinfo.CityName = info.Value.ToString();
                    //}
                    //else if (info.Key.ToLower() == receivingkey && required)
                    //{
                    //    _customerinfo.State = info.Value.ToString();
                    //}
                    //else if (info.Key.ToLower() == receivingkey && required)
                    //{
                    //    _customerinfo.ZipCode = info.Value.ToString();
                    //}

                    if (info.Key.ToLower() == "country")
                    {
                        _customerinfo.CountryName = info.Value.ToString();
                    }
                    else if (info.Key.ToLower() == "country_code")
                    {
                        _customerinfo.CountryCode = info.Value.ToString();
                    }
                    else if (info.Key.ToLower() == "city")
                    {
                        _customerinfo.CityName = info.Value.ToString();
                    }
                    else if (info.Key.ToLower() == "state")
                    {
                        _customerinfo.State = info.Value.ToString();
                    }
                    else if (info.Key.ToLower() == "postcode")
                    {
                        _customerinfo.ZipCode = info.Value.ToString();
                    }
                }
                var customergeometry = innerlocation.SelectToken("geometry");
                innerinfo = JObject.Parse(customergeometry.ToString());
                foreach (var innergeometry in innerinfo)
                {

                    //string receivingkey = "";
                    //bool required = IsRequiredField(receivingparams, innergeometry.Key.ToLower(), ref receivingkey);
                    //if (innergeometry.Key.ToLower() == receivingkey && required)
                    //{
                    //    _customerinfo.Latitude = Convert.ToDouble(innergeometry.Value.ToString());
                    //}
                    //else if (innergeometry.Key.ToLower() == receivingkey && required)
                    //{
                    //    _customerinfo.Longitude = Convert.ToDouble(innergeometry.Value.ToString());
                    //}
                    if (innergeometry.Key.ToLower() == "lat")
                    {
                        _customerinfo.Latitude = Convert.ToDouble(innergeometry.Value.ToString());
                    }
                    else if (innergeometry.Key.ToLower() == "lng")
                    {
                        _customerinfo.Longitude = Convert.ToDouble(innergeometry.Value.ToString());
                    }
                }
            }
            locations.Add(_customerinfo);
            SendEmailToCustomer();
            return locations;
        }

        
        /// <summary>
        /// Mapping fields
        /// </summary>
        /// <param name="receivingparams">Sitecore Field Parameters</param>
        /// <param name="matchparam">Matching Parameter</param>
        /// <param name="receivingkey">Return Matched Key Value</param>
        /// <returns></returns>
        private bool IsRequiredField(Dictionary<string,string> receivingparams,string matchparam,ref string receivingkey)
        {
            foreach (KeyValuePair<string, string> param in receivingparams) // or foreach(book b in books.Values)
            {
                if (param.Value.Equals(matchparam, StringComparison.CurrentCultureIgnoreCase))
                {
                    receivingkey = param.Value;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Perform Backend Task to Send Email/SMS to various Client of Sitecore Application (xDB)
        /// </summary>
        public void SendEmailToCustomer()
        {
            try
            {
                var customers = new TechHooterCustomers()
                {

                };
                SendEmail email = new SendEmail();
                email.SendEmailToUser("TechHooters@gmail.com", "Welcome to TechHooters");
            }
            catch(Exception exp)
            { }
       }

      /// <summary>
      /// Call Third Party Api to Get Client Location Via Latitude and Longitude
      /// </summary>
      /// <param name="model">Passed the Device Location Details</param>
      /// <param name="LAT">Passed the Longitude</param>
      /// <param name="LNG">Passed the Langitude</param>
      /// <returns></returns>
        private string GetCallApi(TechHooterTracking model,string LAT, string LNG)
        {
            string url = model.ApiUrl + model.Format+"?q=" + LAT + "+" + LNG + "&key=" + model.ApiKey;
            //string url = "https://api.opencagedata.com/geocode/v1/json?q=" + LAT + "+" + LNG + "&key=d4b93ad85988411eadef2f5d79bc7a82";
            string text = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            return text;
        }
    }


}