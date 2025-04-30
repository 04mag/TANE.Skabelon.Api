using TANE.Skabelon.Api.Dtos;
using TANE.Skabelon.Api.Models;
using AutoMapper;

namespace TANE.Skabelon.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DagSkabelonModel, DagSkabelonReadDto>().ReverseMap();
            CreateMap<DagSkabelonCreateDto, DagSkabelonModel>().ReverseMap();
            CreateMap<TurSkabelonModel, TurSkabelonReadDto>().ReverseMap();
            CreateMap<TurSkabelonCreateDto, TurSkabelonModel>().ReverseMap();
            CreateMap<RejseplanSkabelonModel, RejseplanSkabelonReadDto>().ReverseMap();
            CreateMap<RejseplanSkabelonCreateDto, RejseplanSkabelonModel>().ReverseMap();
        }
    }
}
