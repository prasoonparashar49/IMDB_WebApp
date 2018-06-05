using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IMDB_WebApp.Startup))]
namespace IMDB_WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
