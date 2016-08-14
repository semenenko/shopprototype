using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopPrototype.Front.Classic.Startup))]
namespace ShopPrototype.Front.Classic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
