using System.Collections.Generic;

namespace MoDemo.Logger
{
	/// <summary>
	/// Контекст протоколирования.
	/// </summary>
	public interface ILoggerContext
	{
		/// <summary>
		/// Метод вызывается при формировании свойств сообщения.
		/// </summary>
		/// <param name="properties">Справочник свойств контекста сообщения.</param>
		void SetContextProperties(IDictionary<object, object> properties);
	}
}
