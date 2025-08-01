using AutoMapper;
using CanThoTravel.Application.Repository;
using CanThoTravel.Domain.Entities.Member;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CanThoTravel.Application.CQRS.Members.Commands
{
    public class AddMemberCommandHandler : IRequestHandler<AddMemberCommand>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public AddMemberCommandHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task Handle(AddMemberCommand request, CancellationToken cancellationToken)
        {
            var memberEntity = _mapper.Map<MemberEntity>(request.Dto);
            await _memberRepository.AddAsync(memberEntity);
        }
    }
}