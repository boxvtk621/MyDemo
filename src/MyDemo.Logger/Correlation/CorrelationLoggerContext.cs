using System.Collections.Generic;

namespace MyDemo.Logger.Correlation
{
	/// <inheritdoc />
	public sealed class CorrelationLoggerContext : ILoggerContext
	{
		/// <summary>
		/// Наименование контекста корреляции.
		/// </summary>
		public const string CorrelationContextName = "CorrelationId";

		/// <summary>
		/// Контекст операции.
		/// </summary>
		private readonly ICorrelationContextAccessor _accessor;

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="CorrelationLoggerContext"/>.
		/// </summary>
		/// <param name="accessor">Контекст операции.</param>
		public CorrelationLoggerContext(ICorrelationContextAccessor accessor)
		{
			_accessor = accessor;
		}

		/// <inheritdoc />
		public void SetContextProperties(IDictionary<object, object> properties)
		{
			if (_accessor.CorrelationContext != null)
			{
				properties[CorrelationContextName] = _accessor.CorrelationContext.TransactionId;
			}
		}
	}

}
