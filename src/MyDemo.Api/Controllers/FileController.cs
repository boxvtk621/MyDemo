using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MyDemo.Business.Features.Download.Command;
using MyDemo.Business.Features.Upload;

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

		/// <summary>
		/// Загружает файл на диск.
		/// </summary>
		/// <param name="file"><see cref="IFormFile"/>.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns></returns>
		[HttpPost("upload")]
		[DisableRequestSizeLimit]
		public Task Upload(IFormFile file, CancellationToken cancellationToken) =>
			_mediator.Send(new UploadCommand.Command { FileStream = file.OpenReadStream() }, cancellationToken);

	}
}
