using System.Collections.Generic;
using System.Linq;

namespace MoDemo.Logger
{
	/// <inheritdoc cref="NLogLogger"/>
	public class NLogLogger<T> : NLogLogger, ILogger<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NLogLogger"/> class.
		/// </summary>
		/// <param name="contexts">Перечисление контекстов логирования.</param>
		public NLogLogger(IEnumerable<ILoggerContext> contexts)
			: base(typeof(T), contexts.ToArray())
		{
		}
	}
}
