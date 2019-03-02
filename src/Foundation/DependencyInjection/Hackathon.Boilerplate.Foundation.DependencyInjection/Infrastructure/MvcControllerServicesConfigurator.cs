namespace Hackathon.Boilerplate.Foundation.DependencyInjection.Infrastructure
{
    using Glass.Mapper.Sc;
    using Glass.Mapper.Sc.Web;
    using Glass.Mapper.Sc.Web.Mvc;
    using Glass.Mapper.Sc.Web.WebForms;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore;
    using Sitecore.DependencyInjection;

    public class MvcControllerServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            //serviceCollection.AddMvcControllers("*.Feature.*");
            //serviceCollection.AddClassesWithServiceAttribute("*.Feature.*");
            serviceCollection.AddClassesWithServiceAttribute("*.Foundation.*");

           
            serviceCollection.AddMvcControllers("Hackathon.Boilerplate.Feature.Navigation");
            serviceCollection.AddMvcControllers("TechHooterTrackingFeature");
            serviceCollection.AddClassesWithServiceAttribute("Hackathon.Boilerplate.Feature.Navigation");
            serviceCollection.AddClassesWithServiceAttribute("TechHooterTrackingFeature");

            serviceCollection.AddScoped<ISitecoreService>(sp => new SitecoreService(Context.Database));
            serviceCollection.AddScoped<IMvcContext>(sp => new MvcContext(sp.GetService<ISitecoreService>()));
            serviceCollection.AddScoped<IRequestContext>(sp => new RequestContext(sp.GetService<ISitecoreService>()));
            serviceCollection.AddScoped<IWebFormsContext>(sp => new WebFormsContext(sp.GetService<ISitecoreService>()));
        }
    }
}