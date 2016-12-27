using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bonafide.Startup))]
namespace Bonafide
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
