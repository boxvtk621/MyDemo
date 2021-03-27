using System;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MyDemo.Logger.Correlation
{
	/// <summary>
	/// Расширения для работы с контекстом операции.
	/// </summary>
	public static class CorrelationContextExtensions
	{
		/// <summary>
		/// Добавляет необходимые службы для поддержки функциональности идентификатора корреляции.
		/// </summary>
		/// <param name="serviceCollection">Коллекция DI.</param>
		/// <returns>Коллекция DI с добавленными службами.</returns>
		[NotNull]
		public static IServiceCollection AddCorrelationContext(
			[NotNull] this IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<CorrelationMiddleware>();
			serviceCollection.AddSingleton<ICorrelationContextManager, CorrelationContextManager>();
			serviceCollection.AddSingleton<ICorrelationContextAccessor>(sp => sp.GetRequiredService<ICorrelationContextManager>());
			serviceCollection.AddTransient<ICorrelationContextFactory, CorrelationContextFactory>();
			serviceCollection.AddTransient<ILoggerContext, CorrelationLoggerContext>();

			return serviceCollection;
		}

		/// <summary>Включает идентификаторы корреляции для запроса.</summary>
		/// <param name="app">Экземпляр фабрики конфигурирования процесса приложения.</param>
		/// <returns>Обработанный экземпляр фабрики конфигурирования процесса приложения.</returns>
		[NotNull]
		public static IApplicationBuilder UseOperationContext(
			[NotNull] this IApplicationBuilder app)
		{
			return app.UseMiddleware<CorrelationMiddleware>();
		}

		/// <summary>
		/// Добавляет необходимые службы для поддержки функциональности идентификатора корреляции.
		/// </summary>
		/// <param name="serviceCollection">Коллекция DI.</param>
		/// <returns>Коллекция DI с добавленными службами.</returns>
		[NotNull]
		public static IServiceCollection AddMvcOperationContext(
			[NotNull] this IServiceCollection serviceCollection)
		{
			return serviceCollection.AddSingleton<CorrelationMiddleware>();
		}
	}
}
