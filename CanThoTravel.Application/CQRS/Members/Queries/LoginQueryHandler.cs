using AutoMapper;
using CanThoTravel.Application.DTOs.Authentication;
using CanThoTravel.Application.DTOs.Member;
using CanThoTravel.Application.IRepositories.Authentication;
using CanThoTravel.Application.Repository;
using MediatR;

namespace CanThoTravel.Application.CQRS.Members.Queries
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResponseDTO>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public LoginQueryHandler(IMemberRepository memberRepository, IAuthService authService, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<AuthResponseDTO> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByEmailAsync(request.Dto.Email);
            if (member == null || !_authService.VerifyPassword(request.Dto.Password, member.PasswordHash))
            {
                throw new Exception("Invalid email or password.");
            }

            var token = _authService.GenerateJwtToken(member);
            var userDto = _mapper.Map<MemberResponseDTO>(member);

            return new AuthResponseDTO { Token = token, User = userDto };
        }
    }
}