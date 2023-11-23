using CloudSuite.Modules.Application.Handlers.DAS.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.DAS
{
    public class CheckDASExistsByDocumentNumberRequestValidation : AbstractValidator<CheckDASExistsByDocumentNumberRequest>
    {
        public CheckDASExistsByDocumentNumberRequestValidation()
        {
            RuleFor(a => a.DocumentNumber)
                .NotNull()
                .WithMessage("O número do documento não pode ser nulo.")
                .MaximumLength(100)
                .WithMessage("O número do documento não pode ter mais de 100 caracteres.");
        }
    }
}
