﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtelierEntertainment.BusinessLogic;
using AtelierEntertainment.WebApi.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace AtelierEntertainment.WebApi
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

            services.AddScoped<IMapper, Mapper>();
            services.AddScoped<OrderService>();
            services.AddScoped<IOrdersBusinessLogic, OrdersBusinessLogic>();

			services.AddSwaggerGen(c =>
            {
	            c.SwaggerDoc("v1", new Info { Title = "Atelier Entertainment API", Version = "v1" });
            });

			services.AddAutomapperProfiles();
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
	        app.UseStaticFiles();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
			}

			app.UseSwagger();
           
            app.UseSwaggerUI(c =>
            {
	            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();

		}
	}
}
