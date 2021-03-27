using System.Collections.Generic;
using JetBrains.Annotations;
using NLog;

namespace MyDemo.Logger
{
	/// <inheritdoc />
	[UsedImplicitly]
	public sealed class MdlcLoggerContext : ILoggerContext
	{
		/// <inheritdoc />
		public void SetContextProperties(IDictionary<object, object> properties)
		{
			foreach (var name in MappedDiagnosticsLogicalContext.GetNames())
			{
				properties[name] = MappedDiagnosticsLogicalContext.GetObject(name);
			}
		}
	}

}
