using CanThoTravel.Application.DTOs.Member;

namespace CanThoTravel.Application.DTOs.Authentication
{
    public class AuthResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public MemberResponseDTO User { get; set; }
    }
}