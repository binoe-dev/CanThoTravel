using AutoMapper;
using CanThoTravel.Application.DTOs.Member;
using CanThoTravel.Domain.Entities.Member;

namespace CanThoTravel.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MemberEntity, MemberResponseDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name));
        }
    }
}