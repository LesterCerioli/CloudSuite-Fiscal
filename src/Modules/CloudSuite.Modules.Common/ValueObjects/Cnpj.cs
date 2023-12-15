using NetDevPack.Domain;

namespace CloudSuite.Modules.Common.ValueObjects
{
    public class Cnpj : ValueObject
    {
        private string _cnpjNumber;

        // Constructor that sets the CNPJ number and performs validation
        public Cnpj(string cnpjNumber)
        {
            SetCnpjNumber(cnpjNumber);
        }

        // Property to access the CNPJ number
        public string CnpjNumber => _cnpjNumber;

        // Private method to set the CNPJ number and validate it
        private void SetCnpjNumber(string cnpjNumber)
        {
            // Check if the provided CNPJ number is valid
            if (!IsValid(cnpjNumber))
                throw new ArgumentException("Invalid CNPJ Number", nameof(cnpjNumber));

            // Set the CNPJ number if it's valid
            _cnpjNumber = cnpjNumber;
        }

        // Implicit conversion from string to Cnpj
        public static implicit operator Cnpj(string value) => new Cnpj(value);

        // Explicit conversion from Cnpj to string
        public static explicit operator string(Cnpj cnpj) => cnpj.CnpjNumber;

        // Private method to validate the CNPJ number
        private bool IsValid(string cnpjNumber)
        {
            // Check if the input is null or empty
            if (string.IsNullOrWhiteSpace(cnpjNumber))
                return false;

            // Remove non-digit characters
            cnpjNumber = new string(cnpjNumber.Where(char.IsDigit).ToArray());

            // CNPJ must have 14 digits
            if (cnpjNumber.Length != 14)
                return false;

            // Check for repeated digits or invalid checksum
            if (IsRepeatedDigits(cnpjNumber) || !IsValidChecksum(cnpjNumber))
                return false;

            return true;
        }

        // Private method to check for repeated digits
        private bool IsRepeatedDigits(string cnpjNumber)
        {
            return cnpjNumber.Distinct().Count() == 1;
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

        //yield return CnpjNumber;: O comando yield return é usado para fornecer um valor à coleção sem encerrar a execução do método. Neste caso, o valor retornado é o CnpjNumber da instância da classe.

        //O propósito desta implementação é fornecer os componentes que serão usados 
        //para verificar a igualdade entre duas instâncias da classe Cnpj.Quando você compara dois objetos de valor, é comum usar todos os seus campos(ou propriedades) para determinar se são iguais.
        //Portanto, o GetEqualityComponents retorna uma coleção contendo o CnpjNumber como o único componente para a comparação de igualdade.Se todos os componentes na coleção forem iguais entre duas
        //instâncias, essas instâncias são consideradas iguais


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CnpjNumber;
        }
    }
}