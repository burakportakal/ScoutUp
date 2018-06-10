using System;
using Microsoft.Owin;
using Owin;
using ScoutUp.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Security.OAuth;
using ScoutUp.Providers;

[assembly: OwinStartup(typeof(ScoutUp.Classes.StartupOwin))]

namespace ScoutUp.Classes
{
    public class StartupOwin
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }
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
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/Users/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);
            var hubConfiguration = new HubConfiguration
            {
                EnableDetailedErrors = true
            };
            app.MapSignalR("/signalr",hubConfiguration);
        }
    }
}
