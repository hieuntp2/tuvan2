using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(tuvantuyensinhsv.v2.Startup))]
namespace tuvantuyensinhsv.v2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
