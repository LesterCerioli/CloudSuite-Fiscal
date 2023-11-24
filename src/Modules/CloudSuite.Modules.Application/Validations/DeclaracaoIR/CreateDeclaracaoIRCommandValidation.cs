using CloudSuite.Modules.Application.Handlers.DeclaracaoIR;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.DeclaracaoIR
{
    public class CreateDeclaracaoIRCommandValidation : AbstractValidator<CreateDeclaracaoIRCommand>
    {
        public CreateDeclaracaoIRCommandValidation() 
        {
            RuleFor(a => a.DeclaracoaNumero)
                .NotNull()
                .WithMessage("O Numero da declaração não pode ser nulo.")
                .MaximumLength(100)
                .WithMessage("O número da declaração não pode ter mais de 100 caracteres.");

            RuleFor(a => a.Cnpj)
                .NotNull()
                .WithMessage("A Contribuição complementar não pode ser nula.")
                .Must(cnpj => IsValidCnpj(cnpj.CnpjNumber))
                .WithMessage("O campo Cnpj é inválido.");

            RuleFor(a => a.Cpf)
                .NotNull()
                .WithMessage("A Contribuição complementar não pode ser nula.")
                .Must(cpf => IsValidCpf(cpf.CpfNumber))
                .WithMessage("O campo Cnpj é inválido.");

            RuleFor(a => a.CompanyName)
                .NotNull()
                .WithMessage("O Nome da Empresa não pode ser nulo.")
                .MaximumLength(100)
                .WithMessage("O nome da empresa não pode ter mais de 100 caracteres.");

            RuleFor(a => a.BusinessHeader)
                .NotNull()
                .WithMessage("O campo não pode ser nulo.")
                .MaximumLength(100)
                .WithMessage("O cabeçalho do negócio não pode ter mais de 100 caracteres.");

            RuleFor(a => a.TotalIncome)
                .NotNull()
                .WithMessage("O esse campo não pode ser nulo.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("A renda total deve ser maior ou igual a 0.");

            RuleFor(a => a.SocialSecurity)
                .NotNull()
                .WithMessage("A Previdencia Social não pode ser nula.")
                .GreaterThanOrEqualTo(0)
                .When(a => a.SocialSecurity.HasValue)
                .WithMessage("A segurança social deve ser maior ou igual a 0.");

            RuleFor(a => a.ComplementContribution)
                .NotNull()
                .WithMessage("A Contribuição complementar não pode ser nula.")
                .GreaterThanOrEqualTo(0)
                .When(a => a.ComplementContribution.HasValue)
                .WithMessage("A contribuição complementar deve ser maior ou igual a 0.");

            RuleFor(a => a.Alimony)
                .NotNull()
                .WithMessage("A Contribuição complementar não pode ser nula.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("A pensão alimentícia deve ser maior ou igual a 0.");

            RuleFor(a => a.TaxWithheld)
                .NotNull()
                .WithMessage("A Contribuição complementar não pode ser nula.")
                .MaximumLength(100)
                .WithMessage("O imposto retido na fonte não pode ter mais de 100 caracteres.");

            RuleFor(a => a.PaidValueToBusiness)
                .NotNull()
                .WithMessage("A Contribuição complementar não pode ser nula.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor pago ao negócio deve ser maior ou igual a 0.");

            RuleFor(a => a.ProfitsDividends)
                .NotNull()
                .WithMessage("A Contribuição complementar não pode ser nula.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Os lucros e dividendos devem ser maior ou igual a 0.");
        }
        private bool IsValidCpf(string cpf)
        {
            //Validacao do CPF
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            if (cpf == "00000000000" || cpf == "11111111111" || cpf == "22222222222" || cpf == "33333333333" || cpf == "44444444444" || cpf == "55555555555" || cpf == "66666666666" || cpf == "77777777777" || cpf == "88888888888" || cpf == "99999999999")
                return false;

            var sum = 0;
            var rest = 0;
            for (var i = 1; i <= 9; i++)
                sum = sum + int.Parse(cpf[i - 1].ToString()) * (11 - i);
            rest = (sum * 10) % 11;

            if ((rest == 10) || (rest == 11))
                rest = 0;
            if (rest != int.Parse(cpf[9].ToString()))
                return false;

            sum = 0;
            for (var i = 1; i <= 10; i++)
                sum = sum + int.Parse(cpf[i - 1].ToString()) * (12 - i);
            rest = (sum * 10) % 11;

            if ((rest == 10) || (rest == 11))
                rest = 0;
            if (rest != int.Parse(cpf[10].ToString()))
                return false;

            return true;
        }

        private bool IsValidCnpj(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            // Remove non-digit characters
            cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");

            // CNPJ must have 14 digits
            if (cnpj.Length != 14)
                return false;

            // Check for repeated digits or invalid checksum
            if (IsRepeatedDigits(cnpj) || !IsValidChecksum(cnpj))
                return false;

            return true;
        }

        private bool IsRepeatedDigits(string cnpjNumber)
        {
            return cnpjNumber == new string(cnpjNumber[0], 14);
        }

        // Private method to validate the CNPJ checksum
        private bool IsValidChecksum(string cnpjNumber)
        {
            var sum = 0;
            var multiplier = 5;

            // Calculate the first checksum digit
            for (int i = 0; i < 12; i++)
            {
                sum += int.Parse(cnpjNumber[i].ToString()) * multiplier;
                multiplier = (multiplier == 2) ? 9 : multiplier - 1;
            }

            var remainder = sum % 11;
            var digit1 = (remainder < 2) ? 0 : 11 - remainder;

            sum = 0;
            multiplier = 6;

            // Calculate the second checksum digit
            for (int i = 0; i < 13; i++)
            {
                sum += int.Parse(cnpjNumber[i].ToString()) * multiplier;
                multiplier = (multiplier == 2) ? 9 : multiplier - 1;
            }

            remainder = sum % 11;
            var digit2 = (remainder < 2) ? 0 : 11 - remainder;

            // Compare the calculated checksum digits with the provided ones
            return (int.Parse(cnpjNumber[12].ToString()) == digit1) && (int.Parse(cnpjNumber[13].ToString()) == digit2);
        }
    }
}
