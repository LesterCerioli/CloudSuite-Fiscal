using NetDevPack.Domain;

namespace CloudSuite.Modules.Common.ValueObjects
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