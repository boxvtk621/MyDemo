using System;
using System.Collections.Generic;

namespace MyDemo.Logger
{
	/// <summary>
	/// Обёртка для сохранения свойств сообщения.
	/// </summary>
	internal sealed class NLogPropertyLogger : NLogLogger
	{
		/// <summary>
		/// Дополнительные свойства сообщения журнала событий.
		/// </summary>
		private readonly IDictionary<string, object> _properties;

		/// <summary>
		/// Initializes a new instance of the <see cref="NLogLogger"/> class.
		/// </summary>
		/// <param name="type">The type to associate with the logger.</param>
		/// <param name="contexts">Перечисление контекстов логирования.</param>
		internal NLogPropertyLogger(Type type, IEnumerable<ILoggerContext> contexts)
			: base(type, contexts)
		{
			_properties = new Dictionary<string, object>();
		}

		/// <summary>
		/// Вспомогательный метод для формирования логирования с дополнительными свойствами.
		/// </summary>
		/// <param name="key">Идентификатор пользовательского свойства.</param>
		/// <param name="value">Значение пользовательского свойства.</param>
		/// <returns>Исходный объект.</returns>
		public override ILogger WithProperty(string key, object value)
		{
			_properties[key] = value;
			return this;
		}

		/// <summary>
		/// Дополняет свойства события пользовательскими свойствами.
		/// </summary>
		/// <param name="properties">Свойства журнала событий.</param>
		protected override void UpdateProperties(IDictionary<object, object> properties)
		{
			base.UpdateProperties(properties);

			// Копируем пользовательские свойства
			foreach (var kvp in _properties)
			{
				properties[kvp.Key] = kvp.Value;
			}
		}
	}

}
