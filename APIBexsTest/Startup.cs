using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BexsTestBS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace APIBexsTest
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IAirportBusiness, AirportBusiness>();
            services.AddScoped<IValidationBusiness, ValidationBusiness>();
            services.AddScoped<ITravelPathBusiness, TravelPathBusiness>();
            services.AddScoped<IReadDestinationFile, ReadDestinationFileCSV>();
            services.AddScoped<IWriteDestinationFile, WriteDestinationFile>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
