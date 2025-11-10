using Application.Dtos.HotelsandLocations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Hotels_and_Locations
{
    public class UpdateHotelandLocationRequestValidator : AbstractValidator<UpdateDTO>
    {
        public UpdateHotelandLocationRequestValidator()
        {
            RuleFor(e => e.Name)
                .MinimumLength(3).WithMessage("The Name Must Be 3 Or More Letters");

            RuleFor(x => x.Place)
                .MinimumLength(3).WithMessage("The Place Must Be 3 Or More Letter");
        }
    }
}
