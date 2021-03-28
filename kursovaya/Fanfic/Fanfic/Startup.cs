using Fanfic.Data;
using Fanfic.Services;
using Fanfic.Services.ChapterOrderedList;
using Fanfic.Services.Filtrator;
using Fanfic.Services.Sorter;
using Fanfic.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using static Fanfic.Services.RoleInitializer;

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
            services.AddTransient<IChaptersListRenumerator, ChaptersRenumerator>();
            services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).
                AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(opt =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru")
                };
                opt.DefaultRequestCulture = new RequestCulture("ru");
                opt.SupportedCultures = supportedCultures;
                opt.SupportedUICultures = supportedCultures;
            });


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
            services.AddCoreAdmin(nameof(RoleType.ADMIN));
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

            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

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
