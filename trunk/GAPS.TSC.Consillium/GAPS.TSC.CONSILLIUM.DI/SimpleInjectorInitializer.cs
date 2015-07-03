using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GAPS.TSC.CONSILLIUM.Services;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Owin;
using SimpleInjector.Extensions;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Integration.Web.Mvc;
using Microsoft.Exchange.WebServices.Data;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Repositories;

namespace GAPS.TSC.CONSILLIUM.DI
{
    public static class SimpleInjectorInitializer
    {

        public static Container InitializeWebApp(IAppBuilder app)
        {
            var container = new Container();

            BuildWebAppDependencies(container, app);

            BuildCommonDependencies(container);

            container.Verify();

            DependencyResolver.SetResolver(
                new SimpleInjectorDependencyResolver(container));

            return container;
        }


        public static void BuildCommonDependencies(Container container)
        {
            //Registering Repositories

            container.RegisterOpenGeneric(typeof(IRepository<>), typeof(SqlRepository<>));

            //Registering Services
            container.Register<IProjectService, ProjectService>();
            container.Register<IUserService, UserService>();
            container.Register<IClientService, ClientService>();           
            container.Register<IMainMastersService, MainMastersService>();          
        }

        public static void BuildWebAppDependencies(Container container, IAppBuilder app)
        {

            /*container.RegisterInitializer<ApplicationUserManager>(
                manager => InitializeUserManager(manager, app));*/

            container.RegisterSingle<IAppBuilder>(app);

            //   container.RegisterPerWebRequest<ApplicationUserManager>();
            container.RegisterPerWebRequest<AppCtx>(() => new AppCtx());
            /* container.RegisterPerWebRequest<IUserStore<
                 UserModel>>(() =>
                     new UserStore<UserModel>(
                         container.GetInstance<AppCtx>()));*/

            container.RegisterPerWebRequest<IAuthenticationManager>(() =>
            {
                IOwinContext context = null;
                try
                {
                    context = HttpContext.Current.GetOwinContext();

                }
                catch (InvalidOperationException)
                {
                    // Please note that the `IsVerifying()` method is 
                    // declared in SimpleInjector.Advanced. 
                    if (container.IsVerifying())
                    {
                        return new FakeAuthenticationManager();
                    }
                    throw;
                }

                return context.Authentication;
            });

            //     container.RegisterPerWebRequest<SignInManager<UserModel, string>, ApplicationSignInManager>();

            container.RegisterPerWebRequest<IUnitOfWork, UnitOfWork>();

            //Register All Controllers
            container.RegisterMvcControllers(
                Assembly.GetExecutingAssembly());
        }




        private static ExchangeService GetExchangeService()
        {
            var service = new ExchangeService
            {
                Credentials = new WebCredentials(
                    System.Configuration.ConfigurationManager.AppSettings["ews.username"],
                    System.Configuration.ConfigurationManager.AppSettings["ews.password"])
            };

            service.AutodiscoverUrl(
                System.Configuration.ConfigurationManager.AppSettings["ews.username"],
                (redirectionUrl) =>
                {
                    // The default for the validation callback is to reject the URL.
                    var result = false;

                    var redirectionUri = new Uri(redirectionUrl);

                    // Validate the contents of the redirection URL. In this simple validation
                    // callback, the redirection URL is considered valid if it is using HTTPS
                    // to encrypt the authentication credentials. 
                    if (redirectionUri.Scheme == "https")
                    {
                        result = true;
                    }
                    return result;
                });

            return service;
        }
    }

    public class FakeAuthenticationManager : IAuthenticationManager
    {
        public AuthenticationResponseChallenge AuthenticationResponseChallenge { get; set; }
        public AuthenticationResponseGrant AuthenticationResponseGrant { get; set; }
        public AuthenticationResponseRevoke AuthenticationResponseRevoke { get; set; }
        public ClaimsPrincipal User { get; set; }

        public Task<IEnumerable<AuthenticateResult>> AuthenticateAsync(string[] authenticationTypes)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticateResult> AuthenticateAsync(string authenticationType)
        {
            throw new NotImplementedException();
        }

        public void Challenge(params string[] authenticationTypes)
        {
            throw new NotImplementedException();
        }

        public void Challenge(AuthenticationProperties properties, params string[] authenticationTypes)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuthenticationDescription> GetAuthenticationTypes(
            Func<AuthenticationDescription, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuthenticationDescription> GetAuthenticationTypes()
        {
            throw new NotImplementedException();
        }

        public void SignIn(params System.Security.Claims.ClaimsIdentity[] identities) { }
        public void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities) { }
        public void SignOut(params string[] authenticationTypes) { }
        public void SignOut(AuthenticationProperties properties, params string[] authenticationTypes) { }
    }
}
