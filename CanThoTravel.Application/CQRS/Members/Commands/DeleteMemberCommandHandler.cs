namespace CanThoTravel.Application.CQRS.Members.Commands
{
    using CanThoTravel.Application.IRepositories.Member;
    using MediatR;

    public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, bool>
    {
        private readonly IMemberRepository _memberRepository;

        public DeleteMemberCommandHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<bool> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            await _memberRepository.DeleteAsync(request.Id);
            return true;
        }
    }
}