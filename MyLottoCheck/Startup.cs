using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyLottoCheck.Startup))]
namespace MyLottoCheck
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
