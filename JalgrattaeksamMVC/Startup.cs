using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JalgrattaeksamMVC.Startup))]
namespace JalgrattaeksamMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
