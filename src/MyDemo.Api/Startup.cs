using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using MyDemo.Api.Common;
using MyDemo.Business.Common;
using MyDemo.Logger;
using MyDemo.Logger.Correlation;
using MyDemo.Logger.Http;

using Newtonsoft.Json.Linq;

namespace MyDemo.Api
{
	/// <summary>
	/// Initialize service dependence.
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Initialize stat.
		/// </summary>
		/// <param name="configuration"><see cref="IConfiguration"/>.</param>
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <inheritdoc cref="IConfiguration"/>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services"><see cref="IServiceCollection"/>.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<HttpLoggerOptions>(Configuration.GetSection("HttpLogger"));

			services
				.AddControllers(options => options.Filters.Add(new ApiExceptionFilter()))
				.AddJsonOptions(options =>
				{
					options.JsonSerializerOptions.IgnoreNullValues = true;
					options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
				});

			services.AddCorrelationContext();
			services.AddSingleton<ILoggerContext, MdlcLoggerContext>().AddTransient((provider => (ILogger)new NLogLogger(typeof(ILogger), provider.GetServices<ILoggerContext>()))).AddTransient(typeof(ILogger<>), typeof(NLogLogger<>));
			services.AddSingleton<HttpLoggerMiddleware>();

			services.AddBusinessServices();
			services.AddSwaggerGen(c =>
				{
					var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

					c.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo Api", Version = "v1" });
					c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));

					c.MapType<JToken>(() => new OpenApiSchema { Type = "object" });

					c.SchemaGeneratorOptions.SchemaIdSelector = type => type.FullName;
				})
				.AddSwaggerGenNewtonsoftSupport();
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app"><see cref="IApplicationBuilder"/>.</param>
		/// <param name="env"><see cref="IWebHostEnvironment"/>.</param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseMiddleware<HttpLoggerMiddleware>();
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyDemo.Api v1"));
			}

			app.UseOperationContext();

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
