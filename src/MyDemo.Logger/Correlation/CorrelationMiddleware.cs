using System;
using System.Globalization;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace MyDemo.Logger.Correlation
{
	/// <summary>
	/// Читает/создает идентификатор транзакции, который затем может быть использован в журналах.
	/// </summary>
	public sealed class CorrelationMiddleware : IMiddleware
	{
		/// <inheritdoc cref="ICorrelationContextFactory" />
		private readonly ICorrelationContextFactory _operationContextFactory;

		/// <summary>
		/// Creates a new instance of the <see cref="ICorrelationContextFactory" />.
		/// </summary>
		/// <param name="operationContextFactory">Correlation context factory.</param>
		public CorrelationMiddleware(ICorrelationContextFactory operationContextFactory) =>
			_operationContextFactory = operationContextFactory;

		/// <inheritdoc />
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			var operationContext = _operationContextFactory.Create(GetTransactionId(context));
			try
			{
				context.Response.OnStarting((() =>
				{
					context.Response.Headers[CorrelationLoggerContext.CorrelationContextName] =
						operationContext.TransactionId;
					return Task.CompletedTask;
				}));

				await next(context).ConfigureAwait(false);
			}
			finally
			{
				_operationContextFactory.Release(operationContext);
			}
		}

		/// <summary>
		/// Извлекает http заголовок определяющий идентификатор транзакции.
		/// </summary>
		/// <param name="context">Контекст http запроса.</param>
		/// <returns>Возвращает извлеченный/сгенерированный идентификатор.</returns>
		private static StringValues GetTransactionId(HttpContext context)
		{
			if (!context.Request.Headers.TryGetValue(CorrelationLoggerContext.CorrelationContextName,
				out var stringValues) || StringValues.IsNullOrEmpty(stringValues))
			{
				stringValues = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);
			}

			return stringValues;
		}
	}
}
