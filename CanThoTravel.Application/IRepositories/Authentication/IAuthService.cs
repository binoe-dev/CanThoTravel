using CanThoTravel.Domain.Entities.Member;

namespace CanThoTravel.Application.IRepositories.Authentication
{
    public interface IAuthService
    {
        string GenerateJwtToken(MemberEntity member);
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
    }
}