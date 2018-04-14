using Owin;

namespace SmartAdminMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigInjector();
        }
    }
}