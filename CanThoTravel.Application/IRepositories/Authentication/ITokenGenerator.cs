using CanThoTravel.Domain.Entities.Member;

namespace CanThoTravel.Application.IRepositories.Authentication
{
    public interface ITokenGenerator
    {
        string GenerateJwtToken(MemberEntity member, string? type = null);
    }
}