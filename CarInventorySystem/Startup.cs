using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarInventorySystem.Startup))]
namespace CarInventorySystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
