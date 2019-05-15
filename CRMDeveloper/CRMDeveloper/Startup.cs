using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore; // пространство имен EntityFramework
using Microsoft.AspNetCore.Authentication.Cookies;

using CRMCore.DB;
using CRMCore.Services;
using CRMCore.Services.Impl;
using CRMCore.Repositories;
using CRMCore.Repositories.Impl;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System.IO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;
using NLog.Extensions.Logging;
using CRMCore.DB.Extensions;
using Microsoft.AspNetCore.Http;
using CRMCore;

namespace CRMDeveloper
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath);

            AppConfiguration = builder.Build();
        }

        public IConfiguration AppConfiguration { get; set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = 1073741824;
                options.MultipartBodyLengthLimit = 1073741824;
                options.MultipartHeadersLengthLimit = 1073741824;
            });

            services.AddResponseCompression();
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.MimeTypes = new[]
                {
                    // Default
                    "text/plain",
                    "text/css",
                    "application/javascript",
                    "text/html",
                    "application/xml",
                    "text/xml",
                    "application/json",
                    "text/json",
                    // Custom
                    "image/svg+xml"
                };
            });

            string connection = Configuration.GetConnectionString("PostgresqlConnection");

            services.AddDbContext<DBContext>(options => options.UseNpgsql(connection));

            

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Login/Index");
                });

            services.AddMvc();

            #region Repository
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ICommentRepository, CommentRepository> ();
            services.AddTransient<IPotentialClientRepository, PotentialClientRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IFileDataRepository, FileDataRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<IParticipantRepository, ParticipantRepository>();
            services.AddTransient<IRoleActivityRepository, RoleActivityRepository>();
            services.AddTransient<IStatusRepository, StatusRepository>();
            services.AddTransient<IPTaskRepository, PTaskRepository>();
            services.AddTransient<IFinanceSubTypeRepository, FinanceSubTypeRepository>();
            services.AddTransient<IFinanceRepository, FinanceRepository>();
            services.AddTransient<IProjectTypeRepository, ProjectTypeRepository>();
            services.AddTransient<ITaskTypeRepository, TaskTypeRepository>();
            services.AddTransient<ISourceRepository, SourceRepository>();
            services.AddTransient<ICustomOptionRepository, CustomOptionsRepository>();
            #endregion

            #region Service
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPotentialClientService, PotentialClientService>();
            services.AddTransient<IDeleteEntityService, DeleteEntitryService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<ICommentsService, CommentService>();
            services.AddTransient<IFileDataService, FileDataService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<IParticipantService, ParticipantService>();
            services.AddTransient<IStatusService, StatusService>();
            services.AddTransient<IFinanceService, FinanceService>();
            services.AddTransient<ISourceService, SourceService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICustomOptionsService, CustomOptionService>();
            services.AddTransient<ICurrentUser, CurrentUser>();
            services.AddTransient<IRootTypesService, RootTypesService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            //SetRootPath
            CoreConfiguration.PathRoot = Configuration.GetSection("RootPath").Value; 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,DBContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            loggerFactory.AddNLog();
            env.ConfigureNLog("nlog.config");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
           
                InitializeDatabase(app);
            SeedData.Initialize(context);

            // при старте проверить все ли папки созданы, и создать их
            ExistAndCreatePath(CoreConfiguration.PathRoot);
            ExistAndCreatePath(CoreConfiguration.PathStorage);
            ExistAndCreatePath(CoreConfiguration.PathAvatar);
        }

        //migrate db in production
        private void InitializeDatabase (IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DBContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        private void ExistAndCreatePath(string path)
        {
            bool exists = System.IO.Directory.Exists(path);

            if (!exists)
                System.IO.Directory.CreateDirectory(path);
        }

    }
}
