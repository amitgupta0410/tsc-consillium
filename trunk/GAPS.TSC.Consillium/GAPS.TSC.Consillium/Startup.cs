using System;
using GAPS.TSC.CONS.DI;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Repositories;
using GAPS.TSC.CONS.Services;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using SimpleInjector;

[assembly: OwinStartupAttribute(typeof(GAPS.TSC.Consillium.Startup))]
namespace GAPS.TSC.Consillium
{
    public partial class Startup
    {
        private SimpleInjector.Container _container;
        public void Configuration(IAppBuilder app)
        {
            _container = SimpleInjectorInitializer.InitializeWebApp(app);
            ConfigureHangfire(app);
            ConfigureAuth(app, _container);
            BackgroundJob.Enqueue(() => InitializeCache());
            //ConfigureAuth(app);
        }


        public void InitializeCache()
        {
            var container = new Container();
            SimpleInjectorInitializer.BuildCommonDependencies(container);
            container.RegisterSingle(() => new AppCtx());
            container.RegisterSingle<IUnitOfWork, UnitOfWork>();

            GlobalConfiguration.Configuration.UseActivator(
                new SimpleInjectorJobActivator(container));

            var userService = container.GetInstance<IUserService>();
            var projectService = container.GetInstance<IProjectService>();
            var clientService = container.GetInstance<IClientService>();
            userService.GetAllUsers();
            projectService.GetAllMasterProjects();
            clientService.GetAllClients();
        }

        private void ConfigureHangfire(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("AppCtx", new SqlServerStorageOptions { });
            app.UseHangfireServer();

            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                AuthorizationFilters = new IAuthorizationFilter[] {
                    new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions {
                        Users = new[] {
                            new BasicAuthAuthorizationUser {
                                Login = "tsc",
                                PasswordClear = "asdf1234"
                            },
                        },
                        RequireSsl = false,
                        SslRedirect = false
                    })
                }
            });


            var container = new Container();

            SimpleInjectorInitializer.BuildCommonDependencies(container);

          container.RegisterSingle(() => new AppCtx());
          container.RegisterSingle<IUnitOfWork, UnitOfWork>();

            GlobalConfiguration.Configuration.UseActivator(
                new SimpleInjectorJobActivator(container));


            container.Verify();
        }
        public class SimpleInjectorJobActivator : JobActivator
        {
            private readonly Container _container;

            public SimpleInjectorJobActivator(Container container)
            {
                if (container == null)
                {
                    throw new ArgumentNullException("container");
                }
                OverrideDefaultInjection(container);
                _container = container;

            }

            public override object ActivateJob(Type jobType)
            {
                return _container.GetInstance(jobType);
            }

            private void OverrideDefaultInjection(Container container)
            {
                container.Options.AllowOverridingRegistrations = true;
                container.RegisterSingle<IUnitOfWork, UnitOfWork>();
                container.RegisterSingle<AppCtx>(() => new AppCtx());

            }
        }
     
    }
}
