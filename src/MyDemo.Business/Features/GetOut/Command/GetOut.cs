using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using FluentValidation;

using MediatR;

namespace MyDemo.Business.Features
{
	/// <summary>
	/// Обработка тестового метода GetOut.
	/// </summary>
	public static class GetOut
	{
		/// <inheritdoc />
		public sealed class Command : IRequest<GetOutResponse>
		{
			/// <see cref="GetOutRequest"/>
			public GetOutRequest Request { get; set; }
		}

		/// <inheritdoc />
		public sealed class Validator : AbstractValidator<Command>
		{
			/// <summary>
			/// Инициализация <see cref="Validator"/>.
			/// </summary>
			public Validator()
			{
				RuleFor(data => data.Request.Name)
					.NotEmpty()
					.WithMessage("Имя должно быть заполнено.");
			}
		}

		/// <inheritdoc />
		public sealed class Handler : IRequestHandler<Command, GetOutResponse>
		{
			/// <inheritdoc cref="IMapper"/>
			private readonly IMapper _mapper;

			/// <summary>
			/// Initialized handler GetOut.
			/// </summary>
			/// <param name="mapper"><see cref="IMapper"/>.</param>
			public Handler(IMapper mapper)
			{
				_mapper = mapper;
			}

			/// <inheritdoc />
			public Task<GetOutResponse> Handle(Command command, CancellationToken cancellationToken)
			{
				return Task.FromResult(_mapper.Map<GetOutResponse>(command.Request));
			}
		}
	}
}
