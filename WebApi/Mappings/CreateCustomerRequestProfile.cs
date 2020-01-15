using AutoMapper;
using WebApi.Entities;
using WebApi.Requests;

namespace WebApi.Mappings
{
	public class CreateCustomerRequestProfile : Profile
	{
		public CreateCustomerRequestProfile()
		{
			CreateMap<CreateCustomerRequest, Customer>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName))
				.ForMember(dest => dest.Id, opt => opt.Ignore());
		}
	}
}
