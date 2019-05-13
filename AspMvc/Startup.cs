using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AspMvc.Startup))]
namespace AspMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
