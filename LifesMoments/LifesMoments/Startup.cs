using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LifesMoments.Startup))]
namespace LifesMoments
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
