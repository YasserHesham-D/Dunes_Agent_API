using Application.Dtos.HotelsandLocations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UpdateHotelRequestValidator : AbstractValidator<UpdateDTO>
    {
        public UpdateHotelRequestValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3).WithMessage("The Hotel Name Must Be 3 Or More Letters");

            RuleFor(x => x.Place)
                .MinimumLength(3).WithMessage("The Hotel Place Must Be 3 Or More Letters");
        }
    }
}
