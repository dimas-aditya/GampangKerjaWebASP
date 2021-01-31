using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(HCMSSMI.App_Start.Startup))]
namespace HCMSSMI.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}