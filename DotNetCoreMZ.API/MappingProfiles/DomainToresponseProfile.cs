using AutoMapper;
using DotNetCoreMZ.API.Contracts.V1.Responses;
using DotNetCoreMZ.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreMZ.API.MappingProfiles
{
    public class DomainToresponseProfile : Profile
    {
        public DomainToresponseProfile()
        {
            CreateMap<Todo, TodoResponse>();
        }
    }
}
