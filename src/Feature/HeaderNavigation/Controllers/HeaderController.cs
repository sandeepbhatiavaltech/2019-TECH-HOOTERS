using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web;
using Glass.Mapper.Sc.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Data.Items;
using Hackathon.Boilerplate.Feature.Navigation.Models;

namespace Hackathon.Boilerplate.Feature.Navigation.Controllers
{
    
    public class HeaderController : Controller
    {
        private readonly IMvcContext mvccontext;
        private readonly IRequestContext requestContext;
        private readonly ISitecoreService sitecoreService;


        public HeaderController(IMvcContext mvccontext,
            IRequestContext requestContext, ISitecoreService sitecoreService)
        {
            this.mvccontext = mvccontext;
            this.requestContext = requestContext;
            this.sitecoreService = sitecoreService;
        }

        

        public ActionResult Index()
        {
            try
            {
                var model = this.mvccontext.GetContextItem<IHomeModel>();
                return View("~/Views/Hackathon/Header/Header.cshtml", model);
            }
            catch (Exception exp)
            {
             
            }
            
            return new EmptyResult();
        }
    }
}