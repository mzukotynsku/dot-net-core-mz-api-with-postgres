using AutoMapper;
using DotNetCoreMZ.API.Domain;
using DotNetCoreMZ.Data.DTO;

namespace DotNetCoreMZ.API.MappingProfiles
{
    public class DtoToDomainProfile : Profile
    {
        public DtoToDomainProfile()
        {
            CreateMap<TodoDTO, Todo>()
                .ReverseMap();

            CreateMap<RefreshTokenDTO, RefreshToken>()
                .ReverseMap();
        }
    }
}
