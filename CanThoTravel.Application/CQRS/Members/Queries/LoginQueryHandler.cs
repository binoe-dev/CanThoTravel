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
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public LoginQueryHandler(IMemberRepository memberRepository, ITokenGenerator tokenGenerator,  IMapper mapper, IPasswordHasher passwordHasher)
        {
            _memberRepository = memberRepository;
            _tokenGenerator = tokenGenerator;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResponseDTO> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByEmailAsync(request.Dto.Email);
            if (member == null || !_passwordHasher.VerifyPassword(request.Dto.Password, member.PasswordHash))
            {
                throw new Exception("Invalid email or password.");
            }

            var token = _tokenGenerator.GenerateJwtToken(member);
            var userDto = _mapper.Map<MemberResponseDTO>(member);

            return new AuthResponseDTO { Token = token, User = userDto };
        }
    }
}