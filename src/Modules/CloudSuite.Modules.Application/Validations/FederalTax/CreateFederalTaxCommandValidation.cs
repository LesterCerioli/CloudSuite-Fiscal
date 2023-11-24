using CloudSuite.Modules.Application.Handlers.FederalTax;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.FederalTax
{
    public class CreateFederalTaxCommandValidation : AbstractValidator<CreateFederalTaxCommand>
    {
        public CreateFederalTaxCommandValidation() 
        {
            RuleFor(a => a.VPIS)
                .NotNull()
                .WithMessage("VPISSpecified não pode ser nulo.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor do VPIS deve ser maior ou igual a 0.");

            RuleFor(a => a.VCOFINS)
                .NotNull()
                .WithMessage("VPISSpecified não pode ser nulo.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor do VCOFINS deve ser maior ou igual a 0.");

            RuleFor(a => a.VIR)
                .NotNull()
                .WithMessage("VPISSpecified não pode ser nulo.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor do VIR deve ser maior ou igual a 0.");

            RuleFor(a => a.VINSS)
                .NotNull()
                .WithMessage("VPISSpecified não pode ser nulo.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor do VINSS deve ser maior ou igual a 0.");

            RuleFor(a => a.VCSLL)
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor do VCSLL deve ser maior ou igual a 0.");

            RuleFor(a => a.VPISSpecified)
                .NotNull()
                .WithMessage("VPISSpecified não pode ser nulo.");

            RuleFor(a => a.VCOFINSSpecified)
                .NotNull()
                .WithMessage("VCOFINSSpecified não pode ser nulo.");

            RuleFor(a => a.VIRSpecified)
                .NotNull()
                .WithMessage("VIRSpecified não pode ser nulo.");

            RuleFor(a => a.VINSSSpecified)
                .NotNull()
                .WithMessage("VINSSSpecified não pode ser nulo.");

            RuleFor(a => a.VCSLLSpecified)
                .NotNull()
                .WithMessage("VCSLLSpecified não pode ser nulo.");
        }
    }
}
