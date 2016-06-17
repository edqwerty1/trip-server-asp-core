using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using TripServer.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TripServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = @"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.AspNetCore.NewDb;Trusted_Connection=True;";
           // var connection = @"Server=localhost;Database=TripServer;Trusted_Connection=True;";
            services.AddDbContext<TripContext>(options => options.UseSqlServer(connection));
            services.AddMvc();
            services.Configure<MvcOptions>(options =>
            {
                var formatter = options.OutputFormatters.First(f => f is JsonOutputFormatter) as JsonOutputFormatter;

                formatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                //formatter.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.;
                formatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowSpecificOrigin"));
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("AllowSpecificOrigin");
            app.UseDefaultFiles();

          app.UseStaticFiles();
            
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
