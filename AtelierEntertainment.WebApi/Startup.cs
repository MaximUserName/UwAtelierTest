using AtelierEntertainment.BusinessLogic;
using AtelierEntertainment.WebApi.Extensions;
using AtelierEntertainment.WebApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
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
            services.AddMvc()
	            .AddJsonOptions(options =>
		            {
			            options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
			            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			            options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
			            options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
			            options.SerializerSettings.DateParseHandling = DateParseHandling.DateTime;
			            //options.SerializerSettings.DateFormatString = DateTimeToStringConverter
			            //options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
			            //options.SerializerSettings.DateFormatString = ;
			            // options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
		            })
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddScoped<IMapper, Mapper>();
            services.AddScoped<OrderService>();
            services.AddScoped<IOrdersBusinessLogic, OrdersBusinessLogic>();

			services.AddSwaggerGen(c =>
            {
	            c.SwaggerDoc("v1", new Info { Title = "Atelier Entertainment API", Version = "v1" });
            });

			services.AddAutomapperProfiles();

			InitializeDatabase();
        }

        private void InitializeDatabase()
        {
	        // OrderDataContext.LoadOrder(1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
	        app.UseMiddleware<ExceptionHandlerMiddleware>();

			app.UseStaticFiles();

			if (env.IsDevelopment())
			{
				// app.UseDeveloperExceptionPage();
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
