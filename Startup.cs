using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.Extensions.Options;

namespace PartyGamesWebApp
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddLocalization(option =>
            {
                option.ResourcesPath = "Resources";
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddViewLocalization(
                    Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix,
                    option =>
                    {
                        option.ResourcesPath = "Resources";
                    })
                .AddDataAnnotationsLocalization();

            services.AddDbContext<DAL.DatabaseContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("PartyGamesDatabase")
                )
            );

            services.Configure<RequestLocalizationOptions>(option =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("pl-PL"),
                    new CultureInfo("en-US")
                };

                option.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pl-PL");
                option.SupportedCultures = supportedCultures;
                option.SupportedUICultures = supportedCultures;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseEndpoints(routes =>
            {
                routes.MapControllerRoute("default", "{controller=Home}/{action=TestIndex}/{id?}");
            });
        }
    }
}
