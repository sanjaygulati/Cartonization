using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cartonization.Web.Startup))]
namespace Cartonization.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
