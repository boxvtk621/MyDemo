
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using MyDemo.Business.Features;

namespace MyDemo.Api.Controllers
{
	/// <inheritdoc />
	[ApiController]
	[Route("test")]
	public class TestController : ControllerBase
	{
		/// <see cref="IMediator"/>
		private readonly IMediator _mediator;

		/// <inheritdoc />
		public TestController(IMediator mediator)
		{
			_mediator = mediator;
		}

		/// <summary>
		/// Тестовый метод для проверки работоспособности медиатора, валидатора и автомаппера.
		/// </summary>
		/// <param name="request"><see cref="GetOutRequest"/>.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns><see cref="GetOutResponse"/>.</returns>
		[HttpPost("getOut")]
		public Task<GetOutResponse> GetOut([FromBody] GetOutRequest request, CancellationToken cancellationToken) =>
			_mediator.Send(new GetOut.Command { Request = request }, cancellationToken);
	}
}
