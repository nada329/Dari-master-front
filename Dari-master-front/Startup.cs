using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dari_master_front.Startup))]
namespace Dari_master_front
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
