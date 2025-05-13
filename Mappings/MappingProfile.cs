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
            CreateMap<DagSkabelonUpdateDto, DagSkabelonModel>().ReverseMap();
            CreateMap<TurSkabelonModel, TurSkabelonReadDto>().ReverseMap();
            CreateMap<TurSkabelonCreateDto, TurSkabelonModel>().ReverseMap();
            CreateMap<TurSkabelonUpdateDto, TurSkabelonModel>()
              //  .EqualityComparison((src, dest) => src.RowVersion == dest.RowVersion)
                .ForMember(d => d.Dage, opt => opt.UseDestinationValue());
            CreateMap<RejseplanSkabelonModel, RejseplanSkabelonReadDto>().ReverseMap();
            CreateMap<RejseplanSkabelonCreateDto, RejseplanSkabelonModel>().ReverseMap();
            CreateMap<RejseplanSkabelonUpdateDto, RejseplanSkabelonModel>()
                .ForMember(d => d.TurSkabeloner, opt => opt.UseDestinationValue());
          
        }
    }
}
