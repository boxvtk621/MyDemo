using System;

using JetBrains.Annotations;

namespace MyDemo.Logger.Correlation
{
	/// <summary>
	/// Интерфейс для управления контекстом запроса.
	/// </summary>
	public interface ICorrelationContextManager : ICorrelationContextAccessor
	{
		/// <summary>
		/// Сохраняет контекст операции в стеке.
		/// </summary>
		/// <param name="operationContext">Контекст операции.</param>
		/// <exception cref="ArgumentNullException">Если <paramref name="operationContext"/> равен <c>null</c>.</exception>
		void Set([NotNull] ICorrelationContext operationContext);

		/// <summary>
		/// Удаляет текущий контекст операции.
		/// </summary>
		/// <param name="operationContext">Контекст операции.</param>
		/// <exception cref="ArgumentNullException">Если <paramref name="operationContext"/> равен <c>null</c>.</exception>
		void Remove([NotNull] ICorrelationContext operationContext);
	}
}
