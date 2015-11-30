using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaVentasWeb.Startup))]
namespace SistemaVentasWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // ConfigureAuth(app);
        }
    }
}
