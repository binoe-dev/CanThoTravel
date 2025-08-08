using CanThoTravel.Application.DTOs.Authentication;
using CanThoTravel.Application.DTOs.Member;
using FluentValidation;

namespace CanThoTravel.Application.Validators.Member
{
    public class RegisterRequestDTOValidator : AbstractValidator<RegisterRequestDTO>
    {
        public RegisterRequestDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
            
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.");
        }
    }
}