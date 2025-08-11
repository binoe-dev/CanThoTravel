using AutoMapper;
using CanThoTravel.Application.DTOs.Authentication;
using CanThoTravel.Application.DTOs.Member;
using CanThoTravel.Application.IRepositories.Authentication;
using CanThoTravel.Application.Repository;
using CanThoTravel.Domain.Entities.Member;
using MediatR;

namespace CanThoTravel.Application.CQRS.Members.Commands
{
    public class RegisterMemberCommandHandler : IRequestHandler<RegisterMemberCommand, AuthResponseDTO>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public RegisterMemberCommandHandler(IMemberRepository memberRepository, ITokenGenerator tokenGenerator, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _tokenGenerator = tokenGenerator;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<AuthResponseDTO> Handle(RegisterMemberCommand request, CancellationToken cancellationToken)
        {
            var existingMember = await _memberRepository.GetByEmailAsync(request.Dto.Email);
            if (existingMember != null)
            {
                throw new Exception("Member with this email already exists.");
            }

            var memberEntity = new MemberEntity
            {
                Name = request.Dto.Name,
                Email = request.Dto.Email,
                Address = request.Dto.Address,
                PasswordHash = _passwordHasher.HashPassword(request.Dto.Password),
                Type = "Regular" // Default type
            };

            var newMemberId = await _memberRepository.AddAsync(memberEntity);
            if (newMemberId <= 0)
            {
                throw new Exception("Failed to register member.");
            }
            memberEntity.Id = newMemberId;

            var token = _tokenGenerator.GenerateJwtToken(memberEntity);
            var userDto = _mapper.Map<MemberResponseDTO>(memberEntity);

            return new AuthResponseDTO { Token = token, User = userDto };
        }
    }
}