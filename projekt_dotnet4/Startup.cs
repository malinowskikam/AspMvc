using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(projekt_dotnet4.Startup))]
namespace projekt_dotnet4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
