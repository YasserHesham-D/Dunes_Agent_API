using Application.Dtos.HotelsandLocations;
using Application.Dtos.Login;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Hotels_and_Locations
{
    public class AddNewHotelandLocationRequestValidator : AbstractValidator<AddNewDTO>
    {
        public AddNewHotelandLocationRequestValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Name is Required")
                .MinimumLength(3).WithMessage("The Name Must Be 3 Or More Letters");

            RuleFor(x => x.Place)
                .NotEmpty().WithMessage("Place is required.")
                .MinimumLength(3).WithMessage("The Place Must Be 3 Or More Letter");
        }
    }
}
