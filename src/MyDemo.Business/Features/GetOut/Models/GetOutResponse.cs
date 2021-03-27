using Newtonsoft.Json;

namespace MyDemo.Business.Features
{
	/// <summary>
	/// Ответ тестового метода медиатора.
	/// </summary>
	public class GetOutResponse
	{
		/// <summary>
		/// Имя в ответе.
		/// </summary>
		[JsonProperty("outName")]
		public string OutName { get; set; }
	}
}
