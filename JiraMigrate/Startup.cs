using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Authentication;
using BusinessLogic;
using Microsoft.AspNetCore.Http;

namespace JiraMigrate
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
            services.AddSession(options =>
            {
                options.Cookie.Name = "JiraMigrate";
                options.IdleTimeout = TimeSpan.FromSeconds(3600);
                options.Cookie.HttpOnly = false;
                options.Cookie.IsEssential = true;
            });
            services.AddControllersWithViews();

            services.AddScoped<BL_Authentication>(provider => 
            new BL_Authentication(Configuration.GetConnectionString("devconnection"))
            );
            services.AddScoped<BL_Todo>(provider =>
            new BL_Todo(Configuration.GetConnectionString("devconnection"))
            );
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<BL_Action>(provider =>
           new BL_Action(Configuration.GetConnectionString("devconnection"))
           );

            services.AddScoped<BL_AddTeam>(provider =>
          new BL_AddTeam(Configuration.GetConnectionString("devconnection"))
          );
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

            app.UseRouting();
            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Authentication}/{action=Login}/{id?}");
            });
        }
    }
}
