using FluentValidation;
using HeavyArchives.Application.DTOs.Concert;
using HeavyArchives.Application.DTOs.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncyclopaediaBisemutum.Application.Validators
{
    public class ManipulateContactDTOValidator:AbstractValidator<ManipulateContactDTO>
    {
        public ManipulateContactDTOValidator()
        {
            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("Invalid email adress, enter a valid one");
        }
    }
}
