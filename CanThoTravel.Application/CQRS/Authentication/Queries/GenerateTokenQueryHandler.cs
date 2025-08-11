using CanThoTravel.Application.IRepositories.Authentication;
using CanThoTravel.Application.Repository;
using MediatR;

namespace CanThoTravel.Application.CQRS.Authentication.Queries
{
    public class GenerateTokenQueryHandler : IRequestHandler<GenerateTokenQuery, string?>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ITokenGenerator _tokenGenerator;

        public GenerateTokenQueryHandler(IMemberRepository memberRepository, ITokenGenerator tokenGenerator)
        {
            _memberRepository = memberRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<string?> Handle(GenerateTokenQuery request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(request.Dto.Id);
            if (member == null)
            {
                return null; // Member not found
            }

            // The Type from the request is added as a claim in the token
            var token = _tokenGenerator.GenerateJwtToken(member, request.Dto.Type);
            return token;
        }
    }
}