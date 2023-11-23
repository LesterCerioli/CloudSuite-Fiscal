using CloudSuite.Modules.Application.Handlers.Prestador;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.Prestador
{
    public class CreatePrestadorCommandValidation : AbstractValidator<CreatePrestadorCommand>
    {
        public CreatePrestadorCommandValidation() 
        {
            RuleFor(a => a.InscricaoMunicipal)
            .MaximumLength(100)
            .WithMessage("A inscrição municipal não pode ter mais de 100 caracteres.");

            RuleFor(a => a.InscricaoEstadual)
                .MaximumLength(100)
                .WithMessage("A inscrição estadual não pode ter mais de 100 caracteres.");

            RuleFor(a => a.DocTomadorEstrangeiro)
                .MaximumLength(100)
                .WithMessage("O documento do tomador estrangeiro não pode ter mais de 100 caracteres.");

            RuleFor(a => a.SocialReason)
                .MaximumLength(100)
                .WithMessage("A razão social não pode ter mais de 100 caracteres.");

            RuleFor(a => a.NomeFantasia)
                .NotNull()
                .WithMessage("O nome fantasia não pode ser nulo.")
                .MaximumLength(100)
                .WithMessage("O nome fantasia não pode ter mais de 100 caracteres.");

            RuleFor(a => a.Tipo)
                .NotNull()
                .WithMessage("O tipo não pode ser nulo.")
                .IsInEnum()
                .WithMessage("O tipo deve ser um valor válido.");

            RuleFor(a => a.Cnpj)
                .Must(cnpj => IsValid(cnpj.CnpjNumber))
                .WithMessage("O campo Cnpj é inválido.");

            RuleFor(a => a.Address.ContactName)
                .NotEmpty()
                .MaximumLength(40)
                .WithMessage("O nome está incorreto.");

            RuleFor(a => a.Address.AddressLine1)
                .NotEmpty()
                .WithMessage("O endereço não pode estar vazio.")
                .MaximumLength(100)
                .WithMessage("O endereço não pode ter mais de 100 caracteres.")
                .MinimumLength(2)
                .WithMessage("O endereço deve ter pelo menos 2 caracteres.")
                .Matches(@"^[a-zA-Z0-9\s,]*$")
                .WithMessage("O endereço só pode conter letras, números, espaços e vírgulas.");

            RuleFor(a => a.Address.City)
                .NotNull()
                .WithMessage("A cidade não pode ser nula.")
                .Must(city => city.CityName.Length <= 100)
                .WithMessage("O nome da cidade não pode ter mais de 100 caracteres.")
                .Must(city => city.CityName.Length >= 2)
                .WithMessage("O nome da cidade deve ter pelo menos 2 caracteres.")
                .Must(city => Regex.IsMatch(city.CityName, @"^[a-zA-Z\s]*$"))
                .WithMessage("O nome da cidade só pode conter letras e espaços.");

            RuleFor(a => a.Address.City.State.StateName)
                .NotNull()
                .WithMessage("O nome do estado não pode ser nulo.")
                .MaximumLength(100)
                .WithMessage("O nome do estado não pode ter mais de 100 caracteres.")
                .MinimumLength(2)
                .WithMessage("O nome do estado deve ter pelo menos 2 caracteres.")
                .Matches(@"^[a-zA-Z\s]*$")
                .WithMessage("O nome do estado só pode conter letras e espaços.");

            RuleFor(a => a.Address.City.State.UF)
                .NotNull()
                .WithMessage("A UF não pode ser nula.")
                .Length(2)
                .WithMessage("A UF deve ter exatamente 2 caracteres.")
                .Matches(@"^[A-Z]*$")
                .WithMessage("A UF só pode conter letras maiúsculas.");

            RuleFor(a => a.Address.City.State.Country.CountryName)
                .NotNull()
                .WithMessage("O nome do país não pode ser nulo.")
                .MaximumLength(100)
                .WithMessage("O nome do país não pode ter mais de 100 caracteres.")
                .MinimumLength(2)
                .WithMessage("O nome do país deve ter pelo menos 2 caracteres.")
                .Matches(@"^[a-zA-Z\s]*$")
                .WithMessage("O nome do país só pode conter letras e espaços.");

            RuleFor(a => a.Address.City.State.Country.Code3)
                .NotNull()
                .WithMessage("O código de 3 letras não pode ser nulo.")
                .Matches(@"^[A-Z]*$")
                .WithMessage("O código de 3 letras só pode conter letras maiúsculas.");

            RuleFor(a => a.Address.City.State.Country.IsBillingEnabled)
                .NotNull()
                .WithMessage("A habilitação para faturamento não pode ser nula.");

            RuleFor(a => a.Address.City.State.Country.IsShippingEnabled)
                .NotNull()
                .WithMessage("A habilitação para envio não pode ser nula.");

            RuleFor(a => a.Address.City.State.Country.IsCityEnabled)
                .NotNull()
                .WithMessage("A habilitação para cidade não pode ser nula.");

            RuleFor(a => a.Address.City.State.Country.IsZipCodeEnabled)
                .NotNull()
                .WithMessage("A habilitação para código postal não pode ser nula.");

            RuleFor(a => a.Address.City.State.Country.IsDistrictEnabled)
                .NotNull()
                .WithMessage("A habilitação para distrito não pode ser nula.");

            RuleFor(a => a.Address.District.Name)
                .NotNull()
                .WithMessage("O nome não pode ser nulo.")
                .MaximumLength(100)
                .WithMessage("O nome não pode ter mais de 100 caracteres.")
                .MinimumLength(2)
                .WithMessage("O nome deve ter pelo menos 2 caracteres.")
                .Matches(@"^[a-zA-Z\s]*$")
                .WithMessage("O nome só pode conter letras e espaços.");

            RuleFor(a => a.Address.District.Type)
                .NotNull()
                .WithMessage("O tipo não pode ser nulo.")
                .MaximumLength(50)
                .WithMessage("O tipo não pode ter mais de 50 caracteres.")
                .Matches(@"^[a-zA-Z\s]*$")
                .WithMessage("O tipo só pode conter letras e espaços.");

            RuleFor(a => a.Address.District.Location)
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
