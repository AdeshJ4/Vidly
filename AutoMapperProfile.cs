using AutoMapper;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile ()
        {
            /*
            source - destination
            CreateMap<SuperHero, SuperHeroDto>(); // Get
            CreateMap<SuperHeroDto, SuperHero>(); // post
            */

            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
        }
    }
}
