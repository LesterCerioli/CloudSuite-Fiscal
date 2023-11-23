using CloudSuite.Modules.Application.Handlers.Note.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.Note
{
    public class CheckNoteExistsByValueRequestValidation : AbstractValidator<CheckNoteExistsByValueRequest>
    {
        public CheckNoteExistsByValueRequestValidation() 
        {
            RuleFor(a => a.Value)
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor deve ser maior ou igual a 0.");
        }
    }
}
