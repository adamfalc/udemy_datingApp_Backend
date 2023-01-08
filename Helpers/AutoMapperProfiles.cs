using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Sqlite.Storage.Internal;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(s => s.IsMain).Url)) //map an individaul property that auto mapper does not know how to deal with
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge())); //rather than calc in the class -- do it with mapping to optimise the sql query

            CreateMap<Photo, PhotoDto>();
        }


    }
}
