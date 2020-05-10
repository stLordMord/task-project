using ApplicationCore;
using Autofac;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<TrainingContext>(options => options.UseSqlServer(Configuration.GetSection("ConnectionStrings").GetSection("DBConnection").Value));

            // Choise Logger
            if (Configuration.GetSection("Service").GetSection("NLog").Value == "true")
            {
                services.AddLogging(builder =>
                {
                    builder.SetMinimumLevel(LogLevel.Trace);
                    builder.AddNLog("nlog.config");
                });
            }
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var connection = Configuration.GetSection("ConnectionStrings").GetSection("DBConnection").Value;
            var UsedEF = Configuration.GetSection("Service").GetSection("EntityFramework").Value;
            var pathLogger = Configuration.GetSection("ConnectionStrings").GetSection("PathLogger").Value;
            var UsedNLog = Configuration.GetSection("Service").GetSection("NLog").Value;

            builder.RegisterModule(new WebModule(pathLogger, UsedNLog));
            builder.RegisterModule(new ServiceModule(connection, UsedEF, pathLogger, UsedNLog));
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
