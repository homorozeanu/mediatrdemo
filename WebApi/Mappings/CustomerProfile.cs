using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.Entities;
using WebApi.Responses;

namespace WebApi.Mappings
{
	public class CustomerProfile : Profile
	{
		public CustomerProfile()
		{
			CreateMap<Customer, CustomerResponse>()
				.ForMember(dest => dest.FullName,
					opt => opt.MapFrom(src => src.Name));
		}
	}
}
