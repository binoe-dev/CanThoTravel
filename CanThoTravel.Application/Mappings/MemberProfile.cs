using AutoMapper;
using CanThoTravel.Application.DTOs.Member;
using CanThoTravel.Domain.Entities.Member;

namespace CanThoTravel.Application.Mappings
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<MemberEntity, MemberResponseDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name));
            CreateMap<AddMemberRequestDTO, MemberEntity>();
            CreateMap<UpdateMemberRequestDTO, MemberEntity>();
        }
    }
}