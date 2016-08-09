using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopPrototype.Startup))]
namespace ShopPrototype
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
