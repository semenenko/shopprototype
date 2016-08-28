using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopPrototype.Front.Angular.Startup))]
namespace ShopPrototype.Front.Angular
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
