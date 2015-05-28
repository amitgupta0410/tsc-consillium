using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GAPS.TSC.Consillium.Startup))]
namespace GAPS.TSC.Consillium
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
