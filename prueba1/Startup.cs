using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(prueba1.Startup))]
namespace prueba1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
