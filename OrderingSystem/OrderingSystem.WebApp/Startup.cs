using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OrderingSystem.WebApp.Startup))]
[assembly: log4net.Config.XmlConfigurator]
namespace OrderingSystem.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            log4net.Config.XmlConfigurator.Configure();
            ConfigureAuth(app);
        }
    }
}
