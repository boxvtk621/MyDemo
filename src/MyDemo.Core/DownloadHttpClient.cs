using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace MyDemo.Core
{
	/// <inheritdoc />
	public class DownloadHttpClient : HttpClient
	{
		/// <summary>
		/// Загружает файл по его адресу.
		/// </summary>
		/// <param name="link">Адрес файла.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns><see cref="FileStreamResult"/>.</returns>
		public async Task<FileStreamResult> DownloadFile(Uri link, CancellationToken cancellationToken)
		{
			var request = new HttpRequestMessage(HttpMethod.Get, link);
			var response = await SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
			response.EnsureSuccessStatusCode();

			var stream = await response.Content.ReadAsStreamAsync();

			var mediaType = response.Content.Headers?.ContentType?.MediaType ?? MediaTypeNames.Application.Octet;

			var result = new FileStreamResult(stream, mediaType)
			{
				FileDownloadName = GetName(response.Content.Headers),
			};

			return result;
		}

		/// <summary>
		/// Извлекает имя файла из ответа по ссылке.
		/// </summary>
		/// <param name="headers"></param>
		/// <returns></returns>
		private string GetName(HttpContentHeaders headers)
		{
			if (string.IsNullOrEmpty(headers?.ContentDisposition?.FileName))
			{
				return NormalizedName(headers?.ContentDisposition?.FileName);
			}

			if (string.IsNullOrEmpty(headers?.ContentDisposition?.FileNameStar))
			{
				return NormalizedName(headers?.ContentDisposition?.FileName);
			}

			return string.Empty;
		}

		/// <summary>
		/// Метод нейтрализует проблемы кодирования.
		/// </summary>
		/// <param name="str">Строка для нормализации.</param>
		/// <returns></returns>
		private string NormalizedName(string str) =>
			Encoding.UTF8.GetString(Array.ConvertAll(str?.ToCharArray() ?? Array.Empty<char>(), c => (byte)c));
	}
}
