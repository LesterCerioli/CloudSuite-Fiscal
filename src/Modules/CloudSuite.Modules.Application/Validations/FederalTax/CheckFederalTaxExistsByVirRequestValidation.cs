﻿using CloudSuite.Modules.Application.Handlers.FederalTax.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.FederalTax
{
    public class CheckFederalTaxExistsByVirRequestValidation : AbstractValidator<CheckFederalTaxExistsByVirRequest>
    {
        public CheckFederalTaxExistsByVirRequestValidation()
        {
            RuleFor(a => a.VIR)
                .NotNull()
                .WithMessage("VPISSpecified não pode ser nulo.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor do VIR deve ser maior ou igual a 0.");
        }
    }
}
