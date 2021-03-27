using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using MyDemo.Core;

namespace MyDemo.Business.Features.Download.Command
{
	/// <summary>
	/// Реализация выгрузки файла по его адресу.
	/// </summary>
	public static class DownloadForLink
	{
		/// <inheritdoc />
		public sealed class Command : IRequest<FileStreamResult>
		{
			/// <summary>
			/// Адрес скачиваемого файла.
			/// </summary>
			public string Link { get; set; }
		}

		/// <inheritdoc />
		public sealed class Handler : IRequestHandler<Command, FileStreamResult>
		{
			/// <inheritdoc cref="DownloadHttpClient"/>
			private readonly DownloadHttpClient _downloadClient;

			/// <summary>
			/// Initialized class
			/// </summary>
			/// <param name="downloadClient"><see cref="DownloadHttpClient"/>.</param>
			public Handler(DownloadHttpClient downloadClient)
			{
				_downloadClient = downloadClient;
			}

			/// <inheritdoc />
			public Task<FileStreamResult> Handle(Command request, CancellationToken cancellationToken) =>
				_downloadClient.DownloadFile(new Uri(HttpUtility.UrlDecode(request.Link, Encoding.UTF8)), cancellationToken);
		}
	}
}
