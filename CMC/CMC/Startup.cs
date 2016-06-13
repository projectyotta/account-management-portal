using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CMC.Startup))]
namespace CMC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
