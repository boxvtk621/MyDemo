using Newtonsoft.Json;

namespace MyDemo.Business.Features
{
	/// <summary>
	/// Запрос тестового метода медиатора.
	/// </summary>
	public class GetOutRequest
	{
		/// <summary>
		/// Имя.
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }
	}
}
