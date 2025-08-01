using AutoMapper;
using CanThoTravel.Application.Repository;
using CanThoTravel.Domain.Entities.Member;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CanThoTravel.Application.CQRS.Members.Commands
{
    public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public UpdateMemberCommandHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            var memberEntity = _mapper.Map<MemberEntity>(request.Dto);
            await _memberRepository.UpdateAsync(memberEntity);
        }
    }
}