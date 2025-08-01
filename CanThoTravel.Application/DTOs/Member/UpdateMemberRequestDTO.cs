namespace CanThoTravel.Application.DTOs.Member
{
    public class UpdateMemberRequestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}