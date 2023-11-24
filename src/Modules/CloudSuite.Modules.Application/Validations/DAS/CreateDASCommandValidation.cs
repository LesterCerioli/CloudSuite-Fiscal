using CloudSuite.Modules.Application.Handlers.DAS;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.DAS
{
    public class CreateDASCommandValidation : AbstractValidator<CreateDASCommand>
    {
        public CreateDASCommandValidation() 
        {
            RuleFor(a => a.ReferenceMonth)
                .NotNull()
                .WithMessage("O mês de referência não pode ser nulo.")
                .MaximumLength(2)
                .WithMessage("O mês de referência não pode ter mais de 2 caracteres.")
                .Matches(@"^(0[1-9]|1[0-2])$")
                .WithMessage("O mês de referência deve ser um valor entre 01 e 12.");

            RuleFor(a => a.DueDate)
                .NotNull()
                .WithMessage("A data de vencimento não pode ser nula.");

            RuleFor(a => a.ReferenceYear)
                .NotNull()
                .WithMessage("O número do documento não pode ser nulo.")
                .MaximumLength(4)
                .WithMessage("O ano de referência não pode ter mais de 4 caracteres.")
                .Matches(@"^\d{4}$")
                .WithMessage("O ano de referência deve ser um número de 4 dígitos.");

            RuleFor(a => a.PaymentValue)
                .NotNull()
                .WithMessage("O número do documento não pode ser nulo.")
                .MaximumLength(30)
                .WithMessage("O valor do pagamento não pode ter mais de 30 caracteres.");

            RuleFor(a => a.DocumentNumber)
                .NotNull()
                .WithMessage("O número do documento não pode ser nulo.")
                .MaximumLength(100)
                .WithMessage("O número do documento não pode ter mais de 100 caracteres.");

            RuleFor(a => a.BarCode)
                .NotNull()
                .WithMessage("O número do documento não pode ser nulo.")
                .MaximumLength(100)
                .WithMessage("O código de barras não pode ter mais de 100 caracteres.");
        }
    }
}
