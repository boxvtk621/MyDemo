using System;
using System.Globalization;

namespace MyDemo.Logger.Correlation
{
	/// <inheritdoc />
	public class CorrelationContext : ICorrelationContext
	{
		/// <inheritdoc />
		public string TransactionId { get; set; } = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);

		/// <inheritdoc />
		public void SetContext(string transactionId)
		{
			TransactionId = transactionId;
		}
	}
}
