using Application.Dtos.HotelsandLocations;
using Application.Dtos.Payment_Methods_and_Status;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Payment_Methods_and_Statuses
{
    public class AddNewMethodandStatusRequestValidator : AbstractValidator<AddNewMethodandStatusDTO>
    {
        public AddNewMethodandStatusRequestValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Name is Required")
                .MinimumLength(3).WithMessage("The Name Must Be 3 Or More Letters");

        }

    }
}
