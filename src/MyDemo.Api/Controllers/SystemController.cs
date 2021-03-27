using Microsoft.AspNetCore.Mvc;

namespace MyDemo.Api.Controllers
{
	/// <inheritdoc />
	[ApiController]
	[Route("system")]
	public class SystemController : ControllerBase
	{
		/// <summary>
		/// Метод для проверки работоспособности сервиса.
		/// </summary>
		/// <returns>Статус ответа сервиса.</returns>
		[HttpGet("ping")]
		public IActionResult Ping()
		{
			return Ok();
		}
	}
}
