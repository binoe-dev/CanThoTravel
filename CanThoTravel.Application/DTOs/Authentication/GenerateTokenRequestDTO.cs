namespace CanThoTravel.Application.DTOs.Authentication
{
    public class GenerateTokenRequestDTO
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}