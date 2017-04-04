using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NeredeYesekNS.Startup))]
namespace NeredeYesekNS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
