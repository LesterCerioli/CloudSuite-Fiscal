using CloudSuite.Modules.Application.Handlers.Address.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.Address
{
    public class CheckAddressExistsByAddressLineRequestValidation : AbstractValidator<CheckAddressExistsByAddressLineRequest>
    {
        public CheckAddressExistsByAddressLineRequestValidation() 
        {
            RuleFor(a => a.AddressLine1)
                .NotEmpty()
                .WithMessage("O endereço não pode estar vazio.")
                .MaximumLength(100)
                .WithMessage("O endereço não pode ter mais de 100 caracteres.")
                .MinimumLength(2)
                .WithMessage("O endereço deve ter pelo menos 2 caracteres.")
                .Matches(@"^[a-zA-Z0-9\s,]*$")
                .WithMessage("O endereço só pode conter letras, números, espaços e vírgulas.");
        }
    }
}
