using KO.Core;
using KO.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KBS
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
            services.AddRazorPages();


            services.AddIdentity<KoUser, IdentityRole>(
                //you set password policy here
                cfg => {
                    cfg.User.RequireUniqueEmail = true;
                    //cfg.Password.RequireDigit = true;
                    //cfg.Password.RequiredLength = 9;
                }).AddEntityFrameworkStores<KnowledgeDbContext>();
            //install Microsoft.EntityFrameworkCore.SqlServer
            services.AddDbContextPool<KnowledgeDbContext>(o =>

                o.UseSqlServer(Configuration.GetConnectionString("KnowledgeDB"))
            );

            services.AddTransient<UserSeeder>();

            services.AddScoped<IKnowledgeData, SqlKnowledgeData>();//services are scoped to a particular http request

            //  services.AddSingleton<IKnowledgeData, InMemoryIKnowledgeData>();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

        //https://docs.microsoft.com/en-us/visualstudio/javascript/npm-package-management?view=vs-2019#npmAdd
        //restore dependencies by clicking on package.json file
            app.UseNodeModules();//this is  a custom pipeline to serve node modules

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            //this fixed the routing for the api controllers
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
