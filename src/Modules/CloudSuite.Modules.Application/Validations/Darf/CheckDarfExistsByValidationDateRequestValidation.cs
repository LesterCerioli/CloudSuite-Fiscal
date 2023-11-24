﻿using CloudSuite.Modules.Application.Handlers.Darf.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.Darf
{
    public class CheckDarfExistsByValidationDateRequestValidation : AbstractValidator<CheckDarfExistsByValidationDateRequest>
    {
        public CheckDarfExistsByValidationDateRequestValidation()
        {
            RuleFor(a => a.ValidationDate)
                .NotNull()
                .WithMessage("A data de validação não pode ser nula.");
        }


    }
}
