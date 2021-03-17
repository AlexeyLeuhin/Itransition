using Fanfic.Data;
using Fanfic.Services;
using Fanfic.Services.Filtrator;
using Fanfic.Services.Sorter;
using Fanfic.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fanfic
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
            services.AddTransient<ITaleFilterService, TaleFiltator>();
            services.AddTransient<ITaleSortService, TaleSorter>();
            services.AddTransient<IBlobService, BlobService>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddAuthentication()
                .AddFacebook(facebookOptions =>
                    {
                facebookOptions.AppId = Configuration["Authentication-Facebook-AppId"];
                facebookOptions.AppSecret = Configuration["Authentication-Facebook-AppSecret"];
                    })
                .AddGoogle(options =>
                    {
                    options.ClientId = Configuration["Authentication-Google-ClientId"];
                    options.ClientSecret = Configuration["Authentication-Google-ClientSecret"];
                    })
                .AddTwitter(twitterOptions =>
                 {
                     twitterOptions.ConsumerKey = Configuration["Authentication-Twitter-ConsumerAPIKey"];
                     twitterOptions.ConsumerSecret = Configuration["Authentication-Twitter-ConsumerSecret"];
                     twitterOptions.RetrieveUserDetails = true;
                 });
            services.AddCoreAdmin();
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<CommentsHub>("/comments");

            });
        }
    }
}
