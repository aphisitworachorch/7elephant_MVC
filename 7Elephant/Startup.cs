using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_7Elephant.Startup))]
namespace _7Elephant
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
