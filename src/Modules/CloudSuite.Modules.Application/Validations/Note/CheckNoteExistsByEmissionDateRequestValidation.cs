using CloudSuite.Modules.Application.Handlers.Note.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.Note
{
    public class CheckNoteExistsByEmissionDateRequestValidation : AbstractValidator<CheckNoteExistsByEmissionDateRequest>
    {
        public CheckNoteExistsByEmissionDateRequestValidation()
        {
            RuleFor(a => a.EmissionDate)
                .LessThanOrEqualTo(DateTime.Now)
                .When(a => a.EmissionDate.HasValue)
                .WithMessage("A data de emissão não pode ser no futuro.");
        }
    }
}
