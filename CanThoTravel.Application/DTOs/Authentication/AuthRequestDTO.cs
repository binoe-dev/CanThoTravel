namespace CanThoTravel.Application.DTOs.Authentication
{
    public class RegisterRequestDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    public class LoginRequestDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}