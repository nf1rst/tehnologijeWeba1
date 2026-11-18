using FluentValidation;
using HeavyArchives.Application.DTOs.Person;
using HeavyArchives.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncyclopaediaBisemutum.Application.Validators
{
    public class ManipulatePersonDTOValidator:AbstractValidator<ManipulatePersonDTO>
    {
        //https://docs.fluentvalidation.net/en/latest/built-in-validators.html
        //https://stackoverflow.com/questions/24839639/fluentvalidation-notempty-message-is-not-showing
        public ManipulatePersonDTOValidator()
        {
            //https://medium.com/@jeslurrahman/fluent-validation-in-net-a-comprehensive-guide-for-beginners-46934b240f13
            RuleFor(a => a.Fname)
                .NotEmpty()
                    .WithMessage("First name cannot be left empty");
            RuleFor(a => a.Lname).NotEmpty().WithMessage("Last name cannot be left empty");
            RuleFor(a => a.Sex).Must(s=>s=='F' ||s=='M').WithMessage("Sex must be M or F");
        }
    }
}
