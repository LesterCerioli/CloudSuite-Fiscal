using CloudSuite.Modules.Application.Handlers.Darf;
using CloudSuite.Modules.Application.Validations.CancelOrder;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.Darf
{
    public class CreateDarfCommandValidation : AbstractValidator<CreateDarfCommand>
    {
        public CreateDarfCommandValidation() 
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
                .WithMessage("O ano de referência não pode ser nulo.")
                .Length(4)
                .WithMessage("O ano de referência deve ter exatamente 4 caracteres.")
                .Matches(@"^\d{4}$")
                .WithMessage("O ano de referência deve ser um número de 4 dígitos.");

            RuleFor(a => a.DarfPaymentValue)
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor do pagamento Darf deve ser maior ou igual a 0.");

            RuleFor(a => a.RecuboDeclaroNumero)
                .MaximumLength(100)
                .WithMessage("O número do recibo Declaro não pode ter mais de 100 caracteres.");

            RuleFor(a => a.DocumentNumber)
                .NotNull()
                .WithMessage("O número do documento não pode ser nulo.")
                .MaximumLength(100)
                .WithMessage("O número do documento não pode ter mais de 100 caracteres.");

            RuleFor(a => a.BarCode)
                .MaximumLength(100)
                .WithMessage("O código de barras não pode ter mais de 100 caracteres.");

            RuleFor(a => a.ValidationDate)
                .NotNull()
                .WithMessage("A data de validação não pode ser nula.");

            RuleFor(a => a.PeriodoApuracao)
                .NotNull()
                .WithMessage("O Periodo Apuracao não pode ser nulo.");

            RuleFor(a => a.Cnpj)
                .NotNull()
                .WithMessage("O Cnpj não pode ser nulo.");

            RuleFor(a => a.ReceitaCode)
                .MaximumLength(100)
                .WithMessage("O código da receita não pode ter mais de 100 caracteres.");

            RuleFor(a => a.MainValue)
                .MaximumLength(30)
                .WithMessage("O valor principal não pode ter mais de 30 caracteres.");

            RuleFor(a => a.AmountFine)
                .GreaterThanOrEqualTo(0)
                .When(a => a.AmountFine.HasValue)
                .WithMessage("O valor da multa deve ser maior ou igual a 0.");

            RuleFor(a => a.IsInstallment)
                .NotNull()
                .WithMessage("A indicação de parcelamento não pode ser nula.");

            RuleFor(a => a.Interest)
                .GreaterThanOrEqualTo(0)
                .When(a => a.Interest.HasValue)
                .WithMessage("O valor do juros deve ser maior ou igual a 0.");

            RuleFor(a => a.TotalValue)
                .GreaterThanOrEqualTo(0)
                .When(a => a.TotalValue.HasValue)
                .WithMessage("O valor total deve ser maior ou igual a 0.");

            RuleFor(a => a.Cnpj)
                .Must(cnpj => IsValid(cnpj.CnpjNumber))
                .WithMessage("O campo Cnpj é inválido.");

            RuleFor(a => a.Prestador.Cnpj)
                .Must(cnpj => IsValid(cnpj.CnpjNumber))
                .WithMessage("O campo Cnpj é inválido.");

            RuleFor(a => a.Prestador.InscricaoMunicipal)
                .MaximumLength(100)
                .WithMessage("A inscrição municipal não pode ter mais de 100 caracteres.");

            RuleFor(a => a.Prestador.InscricaoEstadual)
                .MaximumLength(100)
                .WithMessage("A inscrição estadual não pode ter mais de 100 caracteres.");

            RuleFor(a => a.Prestador.DocTomadorEstrangeiro)
                .MaximumLength(100)
                .WithMessage("O documento do tomador estrangeiro não pode ter mais de 100 caracteres.");

            RuleFor(a => a.Prestador.SocialReason)
                .MaximumLength(100)
                .WithMessage("A razão social não pode ter mais de 100 caracteres.");

            RuleFor(a => a.Prestador.NomeFantasia)
                .NotNull()
                .WithMessage("O nome fantasia não pode ser nulo.")
                .MaximumLength(100)
                .WithMessage("O nome fantasia não pode ter mais de 100 caracteres.");

            RuleFor(a => a.Prestador.Tipo)
                .NotNull()
                .WithMessage("O tipo não pode ser nulo.")
                .IsInEnum()
                .WithMessage("O tipo deve ser um valor válido.");

            RuleFor(a => a.Prestador.Address.ContactName)
                .NotEmpty()
                .MaximumLength(40)
                .WithMessage("O nome está incorreto.");

            RuleFor(a => a.Prestador.Address.AddressLine1)
                .NotEmpty()
                .WithMessage("O endereço não pode estar vazio.")
                .MaximumLength(100)
                .WithMessage("O endereço não pode ter mais de 100 caracteres.")
                .MinimumLength(2)
                .WithMessage("O endereço deve ter pelo menos 2 caracteres.")
                .Matches(@"^[a-zA-Z0-9\s,]*$")
                .WithMessage("O endereço só pode conter letras, números, espaços e vírgulas.");

            RuleFor(a => a.Prestador.Address.City)
                .NotNull()
                .WithMessage("A cidade não pode ser nula.")
                .Must(city => city.CityName.Length <= 100)
                .WithMessage("O nome da cidade não pode ter mais de 100 caracteres.")
                .Must(city => city.CityName.Length >= 2)
                .WithMessage("O nome da cidade deve ter pelo menos 2 caracteres.")
                .Must(city => Regex.IsMatch(city.CityName, @"^[a-zA-Z\s]*$"))
                .WithMessage("O nome da cidade só pode conter letras e espaços.");

            RuleFor(a => a.Prestador.Address.City.State.StateName)
                .NotNull()
                .WithMessage("O nome do estado não pode ser nulo.")
                .MaximumLength(100)
                .WithMessage("O nome do estado não pode ter mais de 100 caracteres.")
                .MinimumLength(2)
                .WithMessage("O nome do estado deve ter pelo menos 2 caracteres.")
                .Matches(@"^[a-zA-Z\s]*$")
                .WithMessage("O nome do estado só pode conter letras e espaços.");

            RuleFor(a => a.Prestador.Address.City.State.UF)
                .NotNull()
                .WithMessage("A UF não pode ser nula.")
                .Length(2)
                .WithMessage("A UF deve ter exatamente 2 caracteres.")
                .Matches(@"^[A-Z]*$")
                .WithMessage("A UF só pode conter letras maiúsculas.");

            RuleFor(a => a.Prestador.Address.District.Name)
                .NotNull()
                .WithMessage("O nome não pode ser nulo.")
                .MaximumLength(100)
                .WithMessage("O nome não pode ter mais de 100 caracteres.")
                .MinimumLength(2)
                .WithMessage("O nome deve ter pelo menos 2 caracteres.")
                .Matches(@"^[a-zA-Z\s]*$")
                .WithMessage("O nome só pode conter letras e espaços.");

            RuleFor(a => a.Prestador.Address.District.Type)
                .NotNull()
                .WithMessage("O tipo não pode ser nulo.")
                .MaximumLength(50)
                .WithMessage("O tipo não pode ter mais de 50 caracteres.")
                .Matches(@"^[a-zA-Z\s]*$")
                .WithMessage("O tipo só pode conter letras e espaços.");

            RuleFor(a => a.Prestador.Address.District.Location)
                .NotNull()
                .WithMessage("A localização não pode ser nula.")
                .MaximumLength(100)
                .WithMessage("A localização não pode ter mais de 100 caracteres.")
                .MinimumLength(10)
                .WithMessage("A localização deve ter pelo menos 10 caracteres.")
                .Matches(@"^[a-zA-Z0-9\s,]*$")
                .WithMessage("A localização só pode conter letras, números, espaços e vírgulas.");


        }

        private bool IsValid(string cnpj)
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
