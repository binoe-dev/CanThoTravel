using AutoMapper;
using CanThoTravel.Application.IRepositories.Member;
using CanThoTravel.Domain.Entities.Member;
using MediatR;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace CanThoTravel.Application.CQRS.Members.Commands
{
    public class AddMemberCommandHandler : IRequestHandler<AddMemberCommand, int>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public AddMemberCommandHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddMemberCommand request, CancellationToken cancellationToken)
        {
            var memberEntity = _mapper.Map<MemberEntity>(request.Dto);
            var result = await _memberRepository.AddAsync(memberEntity);
            if (result <= 0)
            {
                throw new Exception("Failed to add member");
            }
            
            return result;
        }
    }
}