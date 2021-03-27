using System;
using JetBrains.Annotations;

namespace MyDemo.Logger.Correlation
{
	/// <summary>
	/// A factory for creating and disposing an instance of a <see cref="ICorrelationContext"/>.
	/// </summary>
	public interface ICorrelationContextFactory
	{
		/// <summary>
		/// Creates a new <see cref="ICorrelationContext"/> with the correlation ID set for the current request.
		/// </summary>
		/// <param name="transactionId">Идентификатор транзакции для установки в контексте.</param>
		/// <param name="parentTransactionId">Идентификатор родительской транзакции для установки в контексте.</param>
		/// <returns>A new instance of a <see cref="ICorrelationContext"/>.</returns>
		[NotNull]
		ICorrelationContext Create(string transactionId = null, string parentTransactionId = null);

		/// <summary>
		/// Release of the <see cref="ICorrelationContext"/> for the current request.
		/// </summary>
		/// <param name="operationContext">Correlation context.</param>
		/// <exception cref="ArgumentNullException">Если <paramref name="operationContext"/> равен <c>null</c>.</exception>
		void Release([NotNull] ICorrelationContext operationContext);
	}

}
