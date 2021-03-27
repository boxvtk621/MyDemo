using System;
using System.Collections.Generic;
using System.Threading;

namespace MyDemo.Logger.Correlation
{
	/// <inheritdoc />
	public class CorrelationContextManager : ICorrelationContextManager
	{
		/// <summary>
		/// Стек контекстов операций.
		/// </summary>
		private readonly AsyncLocal<Stack<ICorrelationContext>> _contexts = new AsyncLocal<Stack<ICorrelationContext>>();

		/// <inheritdoc />
		public ICorrelationContext CorrelationContext => _contexts.Value?.Count > 0 ? _contexts.Value.Peek() : null;


		/// <inheritdoc />
		public void Set(ICorrelationContext correlationContext)
		{
			if (_contexts.Value == null)
			{
				_contexts.Value = new Stack<ICorrelationContext>();
			}

			_contexts.Value.Push(correlationContext);
		}

		/// <inheritdoc />
		public void Remove(ICorrelationContext correlationContext)
		{
			if (_contexts.Value == null || _contexts.Value.Count == 0)
			{
				throw new InvalidOperationException("Operation context stack is null or empty");
			}
		}
	}
}
