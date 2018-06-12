using LightInject;
using System.Reflection;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SmartAdminMvc.App_Start.Startup))]

namespace SmartAdminMvc.App_Start
{
    public class Startup
    {
        public void ConfigInjector()
        {
            var container = new ServiceContainer();
            container.RegisterAssembly(Assembly.GetExecutingAssembly());
            container.RegisterAssembly("BuildingProject.DataAccess*.dll");
            container.RegisterAssembly("BuildingProject.DataAccessl*.dll");
            container.RegisterControllers();
            container.EnableMvc();
        }

        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
