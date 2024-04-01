using ProductsShop.ProductsShop.EntityModel;
using ProductsShop.Interfaces;
using ProductsShop.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using System;
using ProductsShop.Helper;

namespace ProductsShop
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
            services.AddControllers();


            services.AddControllersWithViews() 
                .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).ConfigureApiBehaviorOptions(options =>
            {
                // Adds a custom error response factory when ModelState is invalid
                //options.InvalidModelStateResponseFactory = InvalidModelStateFilterConvention;

            });
            services.AddRouting(r => r.SuppressCheckForUnhandledSecurityMetadata = true);
            services.AddDbContext<ProductsShopContext>(options =>
                  options.UseSqlServer("Server=Hady-Sharawi\\SQLEXPRESS;Database=ProductsShop;Trusted_Connection=True;MultipleActiveResultSets=true"));
            services.AddTransient<IProductsRepository, ProductsRepository>();
            
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "Frontend";
            });
           


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
          {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

                app.UseCors(builder => builder
               .AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed((host) => true)
               .AllowCredentials()
           );


            app.Use((context, next) =>
            {
                context.Items["__CorsMiddlewareInvoked"] = true;
                return next();
            });



            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "Frontend/src";


                if (env.IsDevelopment())
                {
                    
                    //spa.Options.StartupTimeout = new TimeSpan(0, 5, 0);
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                   // spa.UseProxyToSpaDevelopmentServer("
                }
               
            });

        }
    }
}
