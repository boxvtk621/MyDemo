using System.IO;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace MyDemo.Business.Features.Upload
{
	/// <summary>
	/// Реализация загрузки файла по HTTP на диск.
	/// </summary>
	public static class UploadCommand
	{
		/// <inheritdoc />
		public sealed class Command : IRequest
		{
			/// <inheritdoc cref="IFormFile"/>
			public Stream FileStream { get; set; }
		}

		/// <inheritdoc />
		public sealed class Handler : IRequestHandler<Command>
		{
			/// <inheritdoc />
			public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
			{
				using var stream = File.Create(@".\House_of_Gucci_2021_WEB-DLRip-AVC_MVO_by_Dalemake.mkv");

				await request.FileStream.CopyToAsync(stream);

				return Unit.Value;
			}
		}
	}
}
