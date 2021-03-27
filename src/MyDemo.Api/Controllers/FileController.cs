using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using MyDemo.Business.Features.Download.Command;

namespace MyDemo.Api.Controllers
{
	/// <summary>
	/// Реализация методов работы с файлами.
	/// </summary>
	[ApiController]
	[Route("file")]
	public class FileController : ControllerBase
	{
		private readonly IMediator _mediator;

		/// <summary>
		/// Initialized controller.
		/// </summary>
		/// <param name="mediator"><see cref="IMediator"/>.</param>
		public FileController(IMediator mediator)
		{
			_mediator = mediator;
		}

		/// <summary>
		/// Скачивает файл по ссылке.
		/// </summary>
		/// <param name="linkFile">Ссылка для скачивания файла.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns></returns>
		[HttpGet("download")]
		public Task<FileStreamResult> Download([FromQuery] string linkFile, CancellationToken cancellationToken) =>
			_mediator.Send(new DownloadForLink.Command { Link = linkFile }, cancellationToken);
	}
}
