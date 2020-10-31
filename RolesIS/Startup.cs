using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RolesIS.Startup))]
namespace RolesIS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Services.Cache.Cache.Initialize();

            ConfigureAuth(app);
        }
    }
}
