using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProyectoWEB1.Startup))]
namespace ProyectoWEB1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
