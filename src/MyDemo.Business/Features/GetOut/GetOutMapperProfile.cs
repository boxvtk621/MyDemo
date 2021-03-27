using AutoMapper;

namespace MyDemo.Business.Features
{
	/// <inheritdoc />
	public class GetOutMapperProfile : Profile
	{
		/// <summary>
		/// Initialized profile.
		/// </summary>
		public GetOutMapperProfile()
		{
			base.CreateMap<GetOutRequest, GetOutResponse>()
				.ForMember(d => d.OutName, mo => mo.MapFrom(m => m.Name));
		}
	}
}
