using EncyclopaediaBisemutum.Application.DTOs.Auth;
using FluentValidation;
using HeavyArchives.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncyclopaediaBisemutum.Application.Validators
{
    public class RegisterWithDetailsDTOValidator:AbstractValidator<UserRegisterWithDetailsDTO>
    {
        public RegisterWithDetailsDTOValidator()
        {
            RuleFor(r => r.Age)
                .GreaterThan(0).WithMessage("Your age must be above 0")
                .LessThan(100).WithMessage("You age must be below 100");
        }
    }
}
