using Application.Dtos.Login;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequestDTO>
    {
        public LoginRequestValidator()
        {
            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email Is Required")
                .EmailAddress().WithMessage("Email Is Incorrect .");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password Is Incorrect .");
        }
    }
}
