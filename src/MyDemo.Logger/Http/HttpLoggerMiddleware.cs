using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace MyDemo.Logger.Http
{
	/// <summary>
	/// Только для отладочных работ! В случае работы с файлами приводит в буферизации файлов в памяти.
	/// </summary>
	public class HttpLoggerMiddleware : IMiddleware
	{
		/// <summary>
		/// Значение заголовка скрытое из соображений безопасности.
		/// </summary>
		public static readonly string SecureHeaderValue = "[hidden]";

		/// <inheritdoc cref="ILogger"/>
		private readonly ILogger<HttpLoggerMiddleware> _log;

		/// <inheritdoc cref="HttpLoggerOptions"/>
		private readonly HttpLoggerOptions _options;

		/// <summary>
		/// Creates a new instance of the <see cref="HttpLoggerMiddleware" />.
		/// </summary>
		/// <param name="log"><see cref="ILogger"/>.</param>
		/// <param name="options"><see cref="HttpLoggerOptions"/>.</param>
		public HttpLoggerMiddleware(ILogger<HttpLoggerMiddleware> log, IOptions<HttpLoggerOptions> options)
		{
			_log = log;
			_options = options.Value;
		}

		/// <inheritdoc/>
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			if (!_options.IsDebugRequestData)
			{
				await next(context);

				return;
			}

			context.Request.EnableBuffering();

			var originalResponseStream = context.Response.Body;

			try
			{
				var requestBodyAsString = string.Empty;
				if (context.Request.Headers.ContentLength != 0)
				{
					if (_options.MaxContentLength < context.Request.Headers.ContentLength)
					{
						requestBodyAsString = $"<stream content ({context.Request.Headers.ContentLength} bytes)>";
					}
					else
					{
						requestBodyAsString = await new StreamReader(context.Request.Body).ReadToEndAsync();
					}
				}

				if (context.Request.Body.CanSeek)
				{
					context.Request.Body.Seek(0, SeekOrigin.Begin);
				}

				var responseBodyAsString = string.Empty;

				using var responseMemoryStream = new MemoryStream();
				context.Response.Body = responseMemoryStream;

				await next(context);

				if (context.Response.Headers.ContentLength != 0)
				{
					if (_options.MaxContentLength < context.Response.Headers.ContentLength)
					{
						requestBodyAsString = $"<stream content ({context.Request.Headers.ContentLength} bytes)>";
					}
					else
					{
						responseMemoryStream.Position = 0;
						responseBodyAsString = await new StreamReader(responseMemoryStream).ReadToEndAsync();
						responseMemoryStream.Position = 0;
					}
				}

				await context.Response.Body.CopyToAsync(originalResponseStream);

				var log = _log
					.WithProperty("HttpRequestHeaders", MergeHeaders(context.Request.Headers))
					.WithProperty("HttpResponseHeaders", MergeHeaders(context.Response.Headers));

				if (!string.IsNullOrEmpty(requestBodyAsString) && !string.IsNullOrEmpty(responseBodyAsString))
				{
					log.WithProperty("HttpRequestContent", requestBodyAsString)
						.WithProperty("HttpResponseContent", responseBodyAsString);
				}

				log.Trace($"{context.Request.Method}, {context.Request.Path}");
			}
			finally
			{
				context.Response.Body = originalResponseStream;
			}
		}

		/// <summary>
		/// Объединяет список заголовков в одну строку.
		/// </summary>
		/// <param name="httpHeaders">Список <see cref="HttpHeaders"/>.</param>
		/// <returns>Список заголовков.</returns>
		private string MergeHeaders(IHeaderDictionary httpHeaders)
		{
			var list = new List<string>();

			list = httpHeaders
				.Aggregate(list, (l, p) =>
				{
					var value = _options.SecureHeaders?.Count > 0 &&
								_options.SecureHeaders.Contains(p.Key)
						? SecureHeaderValue
						: string.Join(",", p.Value);

					l.Add($"{p.Key}: {value}");

					return l;
				});

			return string.Join("; \n", list);
		}
	}
}
