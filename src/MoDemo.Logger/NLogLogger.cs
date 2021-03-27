using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

using NLog;

namespace MoDemo.Logger
{
	/// <summary>
	/// A logger that integrates with NLog, passing all messages to a <see cref="Logger"/>.
	/// </summary>
	public class NLogLogger : ILogger
	{
		/// <summary>
		/// Синонимы типов.
		/// </summary>
		private static readonly Dictionary<Type, string> ShorthandMap = new Dictionary<Type, string>
		{
			{ typeof(bool), "bool" },
			{ typeof(byte), "byte" },
			{ typeof(char), "char" },
			{ typeof(decimal), "decimal" },
			{ typeof(double), "double" },
			{ typeof(float), "float" },
			{ typeof(int), "int" },
			{ typeof(long), "long" },
			{ typeof(sbyte), "sbyte" },
			{ typeof(short), "short" },
			{ typeof(string), "string" },
			{ typeof(uint), "uint" },
			{ typeof(ulong), "ulong" },
			{ typeof(ushort), "ushort" },
		};

		/// <summary>
		/// NLog logger.
		/// </summary>
		private readonly NLog.Logger _log;

		/// <summary>
		/// Текущий пользователь.
		/// </summary>
		private readonly IEnumerable<ILoggerContext> _contexts;

		/// <summary>
		/// Initializes a new instance of the <see cref="NLogLogger"/> class.
		/// </summary>
		/// <param name="type">The type to associate with the logger.</param>
		/// <param name="contexts">Перечисление контекстов логирования.</param>
		[SuppressMessage("Design", "CA1062", Justification = "Validated")]
		public NLogLogger(Type type, IEnumerable<ILoggerContext> contexts)
			: this(TypeName(type), contexts)
		{
			Type = type;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NLogLogger"/> class.
		/// </summary>
		/// <param name="name">The type to associate with the logger.</param>
		/// <param name="contexts">Перечисление контекстов логирования.</param>
		public NLogLogger(string name, IEnumerable<ILoggerContext> contexts)
		{
			_contexts = contexts;
			_log = LogManager.GetLogger(name);
		}

		#region ILogger properties

		/// <summary>
		/// Gets the type associated with the logger.
		/// </summary>
		public Type Type { get; }

		/// <summary>
		/// Gets the name of the logger.
		/// </summary>
		public string Name => _log.Name;

		/// <summary>
		/// Gets a value indicating whether messages with Debug severity should be logged.
		/// </summary>
		public bool IsDebugEnabled => _log.IsDebugEnabled;

		/// <summary>
		/// Gets a value indicating whether messages with Info severity should be logged.
		/// </summary>
		public bool IsInfoEnabled => _log.IsInfoEnabled;

		/// <summary>
		/// Gets a value indicating whether messages with Trace severity should be logged.
		/// </summary>
		public bool IsTraceEnabled => _log.IsTraceEnabled;

		/// <summary>
		/// Gets a value indicating whether messages with Warn severity should be logged.
		/// </summary>
		public bool IsWarnEnabled => _log.IsWarnEnabled;

		/// <summary>
		/// Gets a value indicating whether messages with Error severity should be logged.
		/// </summary>
		public bool IsErrorEnabled => _log.IsErrorEnabled;

		/// <summary>
		/// Gets a value indicating whether messages with Fatal severity should be logged.
		/// </summary>
		public bool IsFatalEnabled => _log.IsFatalEnabled;

		#endregion

		#region Common methods

		/// <summary>
		/// Logs the specified exception with Debug severity.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		public void Debug(Exception exception, string format, params object[] args)
		{
			if (IsDebugEnabled)
			{
				Log(LogLevel.Debug, exception, format, args);
			}
		}

		/// <summary>
		/// Logs the specified message with Debug severity.
		/// </summary>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		public void Debug(string format, params object[] args)
		{
			if (IsDebugEnabled)
			{
				Log(LogLevel.Debug, null, format, args);
			}
		}

		/// <summary>
		/// Logs the specified exception with Error severity.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		public void Error(Exception exception, string format, params object[] args)
		{
			if (IsErrorEnabled)
			{
				Log(LogLevel.Error, exception, format, args);
			}
		}

		/// <summary>
		/// Logs the specified message with Error severity.
		/// </summary>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		public void Error(string format, params object[] args)
		{
			if (IsErrorEnabled)
			{
				Log(LogLevel.Error, null, format, args);
			}
		}

		/// <summary>
		/// Logs the specified message with Fatal severity.
		/// </summary>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		public void Fatal(string format, params object[] args)
		{
			if (IsFatalEnabled)
			{
				Log(LogLevel.Fatal, null, format, args);
			}
		}

		/// <summary>
		/// Logs the specified exception with Fatal severity.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		public void Fatal(Exception exception, string format, params object[] args)
		{
			if (IsFatalEnabled)
			{
				Log(LogLevel.Fatal, exception, format, args);
			}
		}

		/// <summary>
		/// Logs the specified message with Info severity.
		/// </summary>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		public void Info(string format, params object[] args)
		{
			if (IsInfoEnabled)
			{
				Log(LogLevel.Info, null, format, args);
			}
		}

		/// <summary>
		/// Logs the specified exception with Info severity.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		public void Info(Exception exception, string format, params object[] args)
		{
			if (IsInfoEnabled)
			{
				Log(LogLevel.Info, exception, format, args);
			}
		}

		/// <summary>
		/// Logs the specified message with Trace severity.
		/// </summary>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		public void Trace(string format, params object[] args)
		{
			if (IsTraceEnabled)
			{
				Log(LogLevel.Trace, null, format, args);
			}
		}

		/// <summary>
		/// Logs the specified exception with Trace severity.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		public void Trace(Exception exception, string format, params object[] args)
		{
			if (IsTraceEnabled)
			{
				Log(LogLevel.Trace, exception, format, args);
			}
		}

		/// <summary>
		/// Logs the specified message with Warn severity.
		/// </summary>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		public void Warn(string format, params object[] args)
		{
			if (IsWarnEnabled)
			{
				Log(LogLevel.Warn, null, format, args);
			}
		}

		/// <summary>
		/// Logs the specified exception with Warn severity.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		public void Warn(Exception exception, string format, params object[] args)
		{
			if (IsWarnEnabled)
			{
				Log(LogLevel.Warn, exception, format, args);
			}
		}

		#endregion

		#region Overrides of LoggerBase

		/// <summary>
		/// Logs the specified message with Debug severity.
		/// </summary>
		/// <param name="message">The message.</param>
		public void Debug(string message)
		{
			if (IsDebugEnabled)
			{
				Log(LogLevel.Debug, null, message);
			}
		}

		/// <summary>
		/// Logs the specified message with Info severity.
		/// </summary>
		/// <param name="message">The message.</param>
		public void Info(string message)
		{
			if (IsInfoEnabled)
			{
				Log(LogLevel.Info, null, message);
			}
		}

		/// <summary>
		/// Logs the specified message with Trace severity.
		/// </summary>
		/// <param name="message">The message.</param>
		public void Trace(string message)
		{
			if (IsTraceEnabled)
			{
				Log(LogLevel.Trace, null, message);
			}
		}

		/// <summary>
		/// Logs the specified message with Warn severity.
		/// </summary>
		/// <param name="message">The message.</param>
		public void Warn(string message)
		{
			if (IsWarnEnabled)
			{
				Log(LogLevel.Warn, null, message);
			}
		}

		/// <summary>
		/// Logs the specified message with Error severity.
		/// </summary>
		/// <param name="message">The message.</param>
		public void Error(string message)
		{
			if (IsErrorEnabled)
			{
				Log(LogLevel.Error, null, message);
			}
		}

		/// <summary>
		/// Logs the specified message with Fatal severity.
		/// </summary>
		/// <param name="message">The message.</param>
		public void Fatal(string message)
		{
			if (IsFatalEnabled)
			{
				Log(LogLevel.Fatal, null, message);
			}
		}

		#endregion

		#region Log Exceptions

		/// <summary>
		/// Logs the specified exception with Debug severity.
		/// </summary>
		/// <param name="message">The message.</param><param name="exception">The exception to log.</param>
		public void DebugException(string message, Exception exception)
		{
			if (IsDebugEnabled)
			{
				Log(LogLevel.Debug, exception, message);
			}
		}

		/// <summary>
		/// Logs the specified exception with Info severity.
		/// </summary>
		/// <param name="message">The message.</param><param name="exception">The exception to log.</param>
		public void InfoException(string message, Exception exception)
		{
			if (IsInfoEnabled)
			{
				Log(LogLevel.Info, exception, message);
			}
		}

		/// <summary>
		/// Logs the specified exception with Trace severity.
		/// </summary>
		/// <param name="message">The message.</param><param name="exception">The exception to log.</param>
		public void TraceException(string message, Exception exception)
		{
			if (IsTraceEnabled)
			{
				Log(LogLevel.Trace, exception, message);
			}
		}

		/// <summary>
		/// Logs the specified message with Warn severity.
		/// </summary>
		/// <param name="message">The message.</param><param name="exception">The exception to log.</param>
		public void WarnException(string message, Exception exception)
		{
			if (IsWarnEnabled)
			{
				Log(LogLevel.Warn, exception, message);
			}
		}

		/// <summary>
		/// Logs the specified exception with Error severity.
		/// </summary>
		/// <param name="message">The message.</param><param name="exception">The exception to log.</param>
		public void ErrorException(string message, Exception exception)
		{
			if (IsErrorEnabled)
			{
				Log(LogLevel.Error, exception, message);
			}
		}

		/// <summary>
		/// Logs the specified exception with Fatal severity.
		/// </summary>
		/// <param name="message">The message.</param><param name="exception">The exception to log.</param>
		public void FatalException(string message, Exception exception)
		{
			if (IsFatalEnabled)
			{
				Log(LogLevel.Fatal, exception, message);
			}
		}

		#endregion

		/// <inheritdoc />
		public IDisposable Scoped(IReadOnlyCollection<(string, object)> items)
		{
			return MappedDiagnosticsLogicalContext.SetScoped(items.Select(item => new KeyValuePair<string, object>(item.Item1, item.Item2)).ToArray());
		}

		#region Virtual members

		/// <summary>
		/// Вспомогательный метод для формирования логирования с дополнительными свойствами.
		/// </summary>
		/// <param name="key">Идентификатор пользовательского свойства.</param>
		/// <param name="value">Значение пользовательского свойства.</param>
		/// <returns>Исходный объект.</returns>
		public virtual ILogger WithProperty(string key, object value)
		{
			return new NLogPropertyLogger(Type, _contexts).WithProperty(key, value);
		}

		/// <summary>
		/// Дополняет свойства события пользовательскими свойствами.
		/// </summary>
		/// <param name="properties">Свойства журнала событий.</param>
		protected virtual void UpdateProperties(IDictionary<object, object> properties)
		{
			foreach (var context in _contexts)
			{
				context.SetContextProperties(properties);
			}
		}

		/// <summary>
		/// Вызывается перед отправкой записи в <see cref="Logger"/>.
		/// </summary>
		/// <param name="eventInfo">Логируемое событие.</param>
		protected virtual void OnBeforeLog(LogEventInfo eventInfo)
		{
		}

		/// <summary>
		/// Вызывается после отправки записи в <see cref="Logger"/>.
		/// </summary>
		/// <param name="eventInfo">Логируемое событие.</param>
		protected virtual void OnAfterLog(LogEventInfo eventInfo)
		{
		}

		#endregion

		/// <summary>
		/// Форматирует имя типа в читаемый вид.
		/// </summary>
		/// <param name="type">Исходный тип.</param>
		/// <returns>Строковое представление имени типа.</returns>
		private static string TypeName(Type type)
		{
			if (type.IsGenericType)
			{
				return type.GetGenericTypeDefinition() == typeof(Nullable<>)
					? string.Concat(TypeName(Nullable.GetUnderlyingType(type)), "?")
					: $"{type.FullName?.Split('`')[0]}<{string.Join(",", type.GenericTypeArguments.Select(TypeName).ToArray())}>";
			}

			if (type.IsArray)
			{
				// ReSharper disable once AssignNullToNotNullAttribute
				return string.Concat(TypeName(type.GetElementType()), "[]");
			}

			return ShorthandMap.ContainsKey(type) ? ShorthandMap[type] : type.FullName;
		}

		/// <summary>
		/// Logs with the specified level while preserving the call site.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The message format.</param>
		/// <param name="args">The arguments.</param>
		private void Log(LogLevel level, Exception exception, string format, params object[] args)
		{
			var le = new LogEventInfo(level, _log.Name, CultureInfo.InvariantCulture, format, args, exception);
			UpdateProperties(le.Properties);

			OnBeforeLog(le);
			_log.Log(typeof(NLogLogger), le);
			OnAfterLog(le);
		}
	}

}
