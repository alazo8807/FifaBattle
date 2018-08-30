using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FifaBattle.Startup))]
namespace FifaBattle
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
