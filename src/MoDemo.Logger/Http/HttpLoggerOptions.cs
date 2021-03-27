using System.Collections.Generic;

namespace MoDemo.Logger.Http
{
	/// <summary>
	/// Настройки протоколирования Http запросов.
	/// </summary>
	public class HttpLoggerOptions
	{
		/// <summary>
		/// Если необходимо протоколировать тела запросов.
		/// </summary>
		public bool IsIncludeContent { get; set; }

		/// <summary>
		/// Если в http запросах используется Chunked, а знать, что внутри хочется.
		/// </summary>
		public bool IsBufferChunkedContent { get; set; }

		/// <summary>
		/// Максимальная длина протоколируемого значения.
		/// </summary>
		public long MaxContentLength { get; set; }

		/// <summary>
		/// Если требуется скрыть заголовок, то нужно указать массив имен этих заголовков.
		/// </summary>
		public IReadOnlyCollection<string> SecureHeaders { get; set; }
	}
}
