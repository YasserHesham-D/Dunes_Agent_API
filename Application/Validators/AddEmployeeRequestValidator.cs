using Application.Dtos.Employee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class AddEmployeeRequestValidator : AbstractValidator<AddEmployeeRequest>
    {
        public AddEmployeeRequestValidator()
        {

                
        }
    }
}
