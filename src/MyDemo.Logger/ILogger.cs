using System;
using System.Collections.Generic;

namespace MyDemo.Logger
{
	/// <summary>
	/// Logs messages to a customizable sink.
	/// </summary>
	public interface ILogger
	{
		/// <summary>
		/// Gets the type associated with the logger.
		/// </summary>
		Type Type { get; }

		/// <summary>
		/// Gets the name of the logger.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets a value indicating whether messages with Debug severity should be logged.
		/// </summary>
		bool IsDebugEnabled { get; }

		/// <summary>
		/// Gets a value indicating whether messages with Info severity should be logged.
		/// </summary>
		bool IsInfoEnabled { get; }

		/// <summary>
		/// Gets a value indicating whether messages with Trace severity should be logged.
		/// </summary>
		bool IsTraceEnabled { get; }

		/// <summary>
		/// Gets a value indicating whether messages with Warn severity should be logged.
		/// </summary>
		bool IsWarnEnabled { get; }

		/// <summary>
		/// Gets a value indicating whether messages with Error severity should be logged.
		/// </summary>
		bool IsErrorEnabled { get; }

		/// <summary>
		/// Gets a value indicating whether messages with Fatal severity should be logged.
		/// </summary>
		bool IsFatalEnabled { get; }

		/// <summary>
		/// Logs the specified message with Debug severity.
		/// </summary>
		/// <param name="message">The message.</param>
		void Debug(string message);

		/// <summary>
		/// Logs the specified message with Debug severity.
		/// </summary>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		[StringFormatMethod("format")]
		void Debug(string format, params object[] args);

		/// <summary>
		/// Logs the specified exception with Debug severity.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		[StringFormatMethod("format")]
		void Debug(Exception exception, string format, params object[] args);

		/// <summary>
		/// Logs the specified exception with Debug severity.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="exception">The exception to log.</param>
		void DebugException(string message, Exception exception);

		/// <summary>
		/// Logs the specified message with Info severity.
		/// </summary>
		/// <param name="message">The message.</param>
		void Info(string message);

		/// <summary>
		/// Logs the specified message with Info severity.
		/// </summary>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		[StringFormatMethod("format")]
		void Info(string format, params object[] args);

		/// <summary>
		/// Logs the specified exception with Info severity.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		[StringFormatMethod("format")]
		void Info(Exception exception, string format, params object[] args);

		/// <summary>
		/// Logs the specified exception with Info severity.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="exception">The exception to log.</param>
		void InfoException(string message, Exception exception);

		/// <summary>
		/// Logs the specified message with Trace severity.
		/// </summary>
		/// <param name="message">The message.</param>
		void Trace(string message);

		/// <summary>
		/// Logs the specified message with Trace severity.
		/// </summary>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		[StringFormatMethod("format")]
		void Trace(string format, params object[] args);

		/// <summary>
		/// Logs the specified exception with Trace severity.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		[StringFormatMethod("format")]
		void Trace(Exception exception, string format, params object[] args);

		/// <summary>
		/// Logs the specified exception with Trace severity.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="exception">The exception to log.</param>
		void TraceException(string message, Exception exception);

		/// <summary>
		/// Logs the specified message with Warn severity.
		/// </summary>
		/// <param name="message">The message.</param>
		void Warn(string message);

		/// <summary>
		/// Logs the specified message with Warn severity.
		/// </summary>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		[StringFormatMethod("format")]
		void Warn(string format, params object[] args);

		/// <summary>
		/// Logs the specified exception with Warn severity.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		[StringFormatMethod("format")]
		void Warn(Exception exception, string format, params object[] args);

		/// <summary>
		/// Logs the specified message with Warn severity.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="exception">The exception to log.</param>
		void WarnException(string message, Exception exception);

		/// <summary>
		/// Logs the specified message with Error severity.
		/// </summary>
		/// <param name="message">The message.</param>
		void Error(string message);

		/// <summary>
		/// Logs the specified message with Error severity.
		/// </summary>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		[StringFormatMethod("format")]
		void Error(string format, params object[] args);

		/// <summary>
		/// Logs the specified exception with Error severity.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		[StringFormatMethod("format")]
		void Error(Exception exception, string format, params object[] args);

		/// <summary>
		/// Logs the specified exception with Error severity.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="exception">The exception to log.</param>
		void ErrorException(string message, Exception exception);

		/// <summary>
		/// Logs the specified message with Fatal severity.
		/// </summary>
		/// <param name="message">The message.</param>
		void Fatal(string message);

		/// <summary>
		/// Logs the specified message with Fatal severity.
		/// </summary>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		[StringFormatMethod("format")]
		void Fatal(string format, params object[] args);

		/// <summary>
		/// Logs the specified exception with Fatal severity.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		/// <param name="format">The message or format template.</param>
		/// <param name="args">Any arguments required for the format template.</param>
		[StringFormatMethod("format")]
		void Fatal(Exception exception, string format, params object[] args);

		/// <summary>
		/// Logs the specified exception with Fatal severity.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="exception">The exception to log.</param>
		void FatalException(string message, Exception exception);

		/// <summary>
		/// Вспомогательный метод для формирования логирования с дополнительными свойствами.
		/// </summary>
		/// <param name="key">Идентификатор пользовательского свойства.</param>
		/// <param name="value">Значение пользовательского свойства.</param>
		/// <returns>Исходный объект.</returns>
		ILogger WithProperty(string key, object value);

		/// <summary>
		/// Добавляет в логический контекст операции несколько значений.
		/// </summary>
		/// <param name="items">Значения, которые надо добавить в контекст операции.</param>
		/// <returns><see cref="IDisposable"/> который используется для удаления значений из контекста.</returns>
		IDisposable Scoped(IReadOnlyCollection<ValueTuple<string, object>> items);
	}

	/// <summary>
	/// A generic interface for logging where the category name is derived from the specified <typeparamref name="T" />
	/// type name. Generally used to enable activation of a named ILogger from dependency injection.
	/// </summary>
	/// <typeparam name="T">Тип категории, для которой будут записываться события.</typeparam>

	public interface ILogger<out T> : ILogger
	{
	}
}
