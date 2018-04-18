using Owin;

namespace BuildingProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigInjector();
        }
    }
}