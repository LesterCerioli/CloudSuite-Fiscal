using NetDevPack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.MultTenant.Fiscal.Domain.Models.Taxes
{
    public class Iss : Entity, IAggregateRoot
    {
        public decimal? IssValue { get; private set; }
    }
}
