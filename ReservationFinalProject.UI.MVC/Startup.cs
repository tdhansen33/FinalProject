using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReservationFinalProject.UI.MVC.Startup))]
namespace ReservationFinalProject.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
