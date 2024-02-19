using NetDevPack.Domain;

namespace CloudSuite.MultTenant.Fiscal.Domain.Models
{
    public class Tenannt : Entity, IAggregateRoot
    {
        public string? TenantNumber { get; provate set; }

        public string? CompanyName { get; private set; }

        public string? Cnpj { get; private set; }

        public Email Email { get; private set; }
    }
}