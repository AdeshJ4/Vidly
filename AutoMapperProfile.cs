using AutoMapper;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile ()
        {

            // Domain to Dto -> Get request
            CreateMap<Customer, CustomerDto>();
            CreateMap<Movie, MovieDto>();
            CreateMap<MembershipType, MembershipTypeDto>();

            // Dto to Domain -> Post request
            CreateMap<CustomerDto, Customer>()
                .ForMember(c=>c.Id, opt=>opt.Ignore());
            CreateMap<MovieDto, Movie>()
                .ForMember(c=> c.Id, opt=>opt.Ignore());



            // Teach Automapper how to map a membershipType to a membershipTypeDTO

        }
    }
}
