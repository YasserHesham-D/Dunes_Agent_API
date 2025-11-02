using Application.Dtos.Login;
using Application.DTOS.Hotels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class AddNewHotelRequestValidator : AbstractValidator<AddNewHotelDTO>
    {
        public AddNewHotelRequestValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Hotel Name is Required")
                .MinimumLength(3).WithMessage("The Hotel Name Must Be 3 Or More Letters");

            RuleFor(x => x.Place)
                .NotEmpty().WithMessage("Hotel Place is required.")
                .MinimumLength(3).WithMessage("The Hotel Place Must Be 3 Or More Letter");
        }
    }
}
