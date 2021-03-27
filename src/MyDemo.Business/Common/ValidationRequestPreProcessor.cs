using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using JetBrains.Annotations;

using MediatR.Pipeline;

namespace MyDemo.Business.Common
{
	/// <summary>
	/// Validates request before execute.
	/// </summary>
	/// <typeparam name="TRequest">Request type.</typeparam>
	[UsedImplicitly]
	public sealed class ValidationRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
	{
		/// <summary>
		/// Requests validator.
		/// </summary>
		private readonly IValidator<TRequest> _validator;

		/// <summary>
		/// Creates an instance of <see cref="ValidationRequestPreProcessor{TRequest}"/>.
		/// </summary>
		/// <param name="validator">Requests validator.</param>
		public ValidationRequestPreProcessor(
			IValidator<TRequest> validator = null)
		{
			_validator = validator;
		}

		/// <inheritdoc />
		public async Task Process(TRequest request, CancellationToken token)
		{
			if (_validator != null)
			{
				await _validator.ValidateAsync(
					request,
					options =>
					{
						options.ThrowOnFailures();
					},
					token);
			}
		}
	}
}
