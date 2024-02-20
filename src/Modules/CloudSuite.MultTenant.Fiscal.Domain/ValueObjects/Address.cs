using NetDevPack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.MultTenant.Fiscal.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public string? Street { get; private set; }

        public string? Number { get; private set; }

        public string? District { get; private set; }

        public string? Complement { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
