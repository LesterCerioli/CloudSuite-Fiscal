using NetDevPack.Domain;

namespace CloudSuite.Modules.Common.ValueObjects
{
    public class PostalCode : ValueObject
    {
        public string Code { get; private set; }

        // Construtor privado para garantir que a instância só pode ser criada internamente
        public PostalCode() { }

        public PostalCode(string code)
        {
            Code = code;
        }

        // Método de criação para garantir validações
        public static PostalCode Create(string code)
        {
            if (string.IsNullOrEmpty(code))
            
                throw new DomainException("O código postal não pode ser vazio.")

                ValidateBrazilianPostalCode(code);
                ValidateUSPostalCode(code);

                return new PostalCode(code);
            
        }

        private static void ValidateBrazilianPostalCode(string code)
        {
            if (code.Length != 8)
                throw new DomainException("O código postal brasileiro deve ter 8 dígitos.");
        
        
        }

        private static void ValidateUSPostalCode(string code)
        {
            if (code.Length != 5)
                throw new DomainException("O código postal dos EUA deve ter 5 dígitos.");
        
        
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}