namespace MyDemo.Logger.Correlation
{
	/// <summary>
	/// Интерфейс контекста корреляции.
	/// </summary>
	public interface ICorrelationContext
	{
		/// <summary>
		/// Идентификатор транзакции.
		/// </summary>
		string TransactionId { get; }

		/// <summary>
		/// Задаёт контекст операции, метод должен вызываться только один раз.
		/// </summary>
		/// <param name="transactionId">Идентификатор транзакции.</param>
		void SetContext(string transactionId);
	}
}
