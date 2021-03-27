namespace MyDemo.Logger.Correlation
{
	/// <inheritdoc />
	public class CorrelationContextFactory : ICorrelationContextFactory
	{
		/// <inheritdoc cref="ICorrelationContextManager"/>
		private readonly ICorrelationContextManager _correlationContextManager;

		/// <summary>
		/// Initializes a new instance of the <see cref="CorrelationContextFactory"/> class.
		/// </summary>
		/// <param name="correlationContextManager">
		/// The <see cref="ICorrelationContextAccessor"/> through which the <see cref="CorrelationContext"/> will be set.
		/// </param>
		public CorrelationContextFactory(ICorrelationContextManager correlationContextManager)
		{
			_correlationContextManager = correlationContextManager;
		}

		/// <inheritdoc />
		public ICorrelationContext Create(string transactionId, string parentTransactionId)
		{
			var correlationContext = new CorrelationContext();
			correlationContext.SetContext(transactionId ?? correlationContext.TransactionId);

			_correlationContextManager.Set(correlationContext);

			return correlationContext;
		}

		/// <inheritdoc />
		public void Release(ICorrelationContext correlationContext)
		{
			_correlationContextManager.Remove(correlationContext);
		}
	}
}
