using CanThoTravel.Domain.Entities.Member;
using MediatR;
using System.Collections.Generic;

namespace CanThoTravel.Application.CQRS.Members.Queries
{
    public record GetAllMembersQuery : IRequest<List<MemberEntity>>;
}