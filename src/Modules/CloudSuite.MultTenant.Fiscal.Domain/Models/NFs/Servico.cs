using CloudSuite.MultTenant.Fiscal.Domain.Models.Taxes;
using NetDevPack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.MultTenant.Fiscal.Domain.Models.NFs
{
    public class Servico : Entity, IAggregateRoot
    {
        public string? CodeService { get; private set; }

        public string? Description { get; private set; }

        public ValorServico ServiceValue { get; private set; }

        public decimal? TotalValue { get; private set; }

        public decimal? DeducaoValor { get; private set; }

        public Iss IssTax { get; private set; }

        public decimal? Aliquota { get; private set; }

        public decimal BaseCalculo { get; private set; }

        public decimal DescontoCondicionado { get; private set; }

        public decimal DescontoIncondicionado { get; private set; }

        public string? Discriminacao { get; private set; }
    }
}
