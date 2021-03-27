using System;
using System.Net;

using FluentValidation;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyDemo.Api.Common
{
	/// <summary>
	/// Обработчик стандартных исключений.
	/// </summary>
	public sealed class ApiExceptionFilter : IExceptionFilter
	{
		/// <inheritdoc />
		public void OnException(ExceptionContext context)
		{
			switch (context.Exception)
			{
				case ValidationException exception:
					context.Result = CreateResultProblemDetails(
						HttpStatusCode.BadRequest,
						exception.Source,
						exception.Message);
					break;
				default:
					context.Result = CreateResultProblemDetails(
						HttpStatusCode.InternalServerError,
						context.Exception.Source,
						context.Exception.Message);
					break;
			}
		}

		/// <summary>
		/// Сформировать результат.
		/// </summary>
		/// <param name="statusCode">Код ошибки.</param>
		/// <param name="errorText">Текст ошибки.</param>
		/// <param name="detail">Детальное описание ошибки.</param>
		/// <returns>Результат.</returns>
		private static ObjectResult CreateResultProblemDetails(HttpStatusCode statusCode, string errorText, string detail)
		{
			var error = new ProblemDetails
			{
				Status = (int)statusCode,
				Title = errorText ?? "Unhandled exception",
				Detail = detail ?? "Unhandled exception",
			};

			return new ObjectResult(error)
			{
				StatusCode = error.Status,
			};
		}
	}
}
