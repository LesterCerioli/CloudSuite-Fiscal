using CloudSuite.Modules.Application.Handlers.Note.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.Note
{
    public class CheckNoteExistsByNoteNumberRequestValidation : AbstractValidator<CheckNoteExistsByNoteNumberRequest>
    {
        public CheckNoteExistsByNoteNumberRequestValidation() 
        {
            RuleFor(a => a.NoteNumber)
                .MaximumLength(100)
                .WithMessage("O número da nota não pode ter mais de 100 caracteres.");
        }
    }
}
