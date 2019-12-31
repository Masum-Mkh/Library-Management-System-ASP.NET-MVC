using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectWebApplicationMVC.Startup))]
namespace ProjectWebApplicationMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
