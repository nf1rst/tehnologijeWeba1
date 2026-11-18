using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncyclopaediaBisemutum.Application.Validators
{
    public class StringNotEmptyValidator:AbstractValidator<string?>
    {
        public StringNotEmptyValidator()
        {
            RuleFor(s => s)
                .NotNull().WithMessage("String input value cannot be null, enter a valid value")
                .NotEmpty().WithMessage("String input value cannot be left blank, enter a valid value");
        }
    }
}
