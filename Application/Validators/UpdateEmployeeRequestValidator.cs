using Application.Dtos.Employee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeRequest>
    {
        public UpdateEmployeeRequestValidator()
        {
            RuleFor(u => u.UserName)
                .MinimumLength(3).WithMessage("UserName Can't be less Than 3 chars");
        }
    }
}
