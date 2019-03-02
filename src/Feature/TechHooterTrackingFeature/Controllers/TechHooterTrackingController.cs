using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web;
using Glass.Mapper.Sc.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TechHooterTrackingFeature.Constant;
using TechHooterTrackingFeature.Models;
using TechHooterTrackingFeature.Repositories;
using TechHooterTrackingFeature.ViewModel;


namespace TechHooterTrackingFeature.Controllers
{
    public class TechHooterTrackingController : Controller
    {
        private readonly IMvcContext mvccontext;
        private readonly IRequestContext requestContext;
        private readonly ISitecoreService sitecoreService;

        public TechHooterTrackingController(IMvcContext mvccontext, IRequestContext requestContext, ISitecoreService sitecoreService)
        {
            this.mvccontext = mvccontext;
            this.requestContext = requestContext;
            this.sitecoreService = sitecoreService;
        }

        // GET: TechHooterTracking
        public ActionResult Index()
        {
            try
            {

                var model = this.sitecoreService.GetItem<TechHooterTracking>(TechHooterConstant.TrackingTemplateID);
                var viewmodel = new TrackUserViewModel()
                {
                    TrackCustomer = model
                };
                
             
                return View(viewmodel);
            }
            catch (Exception exp)
            {
               
            }

            return new EmptyResult();
        }

        /// <summary>
        /// When Client click on any Event from Device (Desktop/Mobile/Tablet)
        /// </summary>
        /// <param name="Latitude">Pass the Client Browser Latitude Value</param>
        /// <param name="Longitude">Pass the Client Browser Longitude Value</param>
        /// <param name="action">Pass the Action Event</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string Latitude, string Longitude,string action)
        {
            try
            {
                Latitude = "28.7058088";
                Longitude = "77.1037727";
                if (Latitude != "" && Longitude != "")
                {
                    var model = this.sitecoreService.GetItem<TechHooterTracking>(TechHooterConstant.TrackingTemplateID);
                    TechHooterTrackingRepositories repo = new TechHooterTrackingRepositories();
                    var CustomerList = repo.SaveEventAction(model, Latitude, Longitude, action);
                   
                    var viewmodel = new TrackUserViewModel()
                    {
                        TrackCustomer = model,
                        Customers= CustomerList
                    };
                    //TechHooterTrackingRepositories repo = new TechHooterTrackingRepositories();
                    //var CustomerList=repo.SaveEventAction(model, "28.7058088", "77.1037727", "Shopping");
                    Session["UserInfo"] = CustomerList;
                    Response.Redirect("~/AboutUs");
                    //return View("~/Views/TechHooterTracking/AboutUs.cshtml", viewmodel);
                }
                return new EmptyResult();
            }
            catch (Exception exp)
            { }
            return new EmptyResult();
        }

        /// <summary>
        /// To Display the details about customer
        /// </summary>
        /// <returns></returns>
        public ActionResult AboutUs()
        {
            try
            {

                var model = this.sitecoreService.GetItem<TechHooterTracking>(TechHooterConstant.TrackingTemplateID);
                
                var viewmodel = new TrackUserViewModel()
                {
                    TrackCustomer = model
                };
               
                if (Session["UserInfo"] != null)
                {
                    viewmodel.Customers = (dynamic)Session["UserInfo"];
                }
                //TechHooterTrackingRepositories repo = new TechHooterTrackingRepositories();
                //var CustomerList=repo.SaveEventAction(model, "28.7058088", "77.1037727", "Shopping");
                return View(viewmodel);
            }
            catch (Exception exp)
            {

            }

            return new EmptyResult();
        }
    }
}