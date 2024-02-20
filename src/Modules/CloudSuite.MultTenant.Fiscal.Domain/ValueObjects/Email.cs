using NetDevPack.Domain;

namespace CloudSuite.MultTenant.Fiscal.Domain.ValueObjects
{
    public class Email : ValueObject
    {

        public string? EmailAddress { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }

    }
}