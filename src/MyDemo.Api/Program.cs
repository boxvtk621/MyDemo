using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog.Extensions.Logging;

namespace MyDemo.Api
{
	/// <summary>
	/// Start this api service.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// Main method.
		/// </summary>
		/// <param name="args"></param>
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		/// <summary>
		/// Create service.
		/// </summary>
		/// <param name="args">String array arguments.</param>
		/// <returns><see cref="IHostBuilder"/>.</returns>
		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				})
				.ConfigureLogging((ctx, logging) =>
				{
					NLog.LogManager.Configuration = new NLogLoggingConfiguration(ctx.Configuration.GetSection("NLog"));

					logging
						.SetMinimumLevel(LogLevel.Trace)
						.ClearProviders()
						.AddNLog();
				});
	}
}
