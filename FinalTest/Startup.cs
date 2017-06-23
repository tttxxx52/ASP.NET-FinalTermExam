using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinalTest.Startup))]
namespace FinalTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
