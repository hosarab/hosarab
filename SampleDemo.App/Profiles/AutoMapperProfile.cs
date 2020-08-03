using AutoMapper;
using SampleDemo.Dtos;
using SampleDemo.Models;

namespace SampleDemo.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Source -> Target
            CreateMap<Person, PersonReadDto>();
            CreateMap<PersonCreateDto, Person>();
            CreateMap<PersonUpdateDto, Person>();
            CreateMap<Person, PersonUpdateDto>();
        }
    }
    
}