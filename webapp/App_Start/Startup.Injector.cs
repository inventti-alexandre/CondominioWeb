using LightInject;
using System.Reflection;
using Owin;

namespace BuildingProject.App_Start
{
    public partial class Startup
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
