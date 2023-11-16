using CloudSuite.Modules.Common.Enums;
using CloudSuite.Modules.Common.ValueObjects;

namespace CloudSuite.Modules.Domain.Models
{
    public class Note
    {
        
        

        public TomadorServico TomadorServico { get; private set; }

        public Address Address { get; private private set; }

        public Country Country { get; private set; }

        public District District { get; private set; }

        public Prestador Prestador { get; private set; }
        
    }
}