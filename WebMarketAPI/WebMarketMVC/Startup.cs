using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebMarketMVC.Startup))]
namespace WebMarketMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
