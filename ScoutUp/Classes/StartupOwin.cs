using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using ScoutUp.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.Owin;

[assembly: OwinStartup(typeof(ScoutUp.Classes.StartupOwin))]

namespace ScoutUp.Classes
{
    public class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new ScoutUpDB());
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<RoleManager<AppRole>>((options, context) =>
                new RoleManager<AppRole>(
                    new RoleStore<AppRole>(context.Get<ScoutUpDB>())));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/"),
            });
        }
    }
}
