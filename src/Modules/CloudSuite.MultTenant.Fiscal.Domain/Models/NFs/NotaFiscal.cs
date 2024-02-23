using NetDevPack.Domain;

namespace CloudSuite.MultTenant.Fiscal.Domain.Models.NFs
{
    public class NotaFiscal : Entity, IAggregateRoot
    {

        public string? NotaNumber { get; set; }
        
    }
}