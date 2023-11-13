using NetDevPack.Domain;

namespace CloudSuite.Modules.Domain.Models
{
    public class FederalTax : Entity, IAggregateRoot
    {
        public FederalTax(decimal? vPIS, decimal? vCOFINS, decimal? vIR, decimal? vINSS, decimal? vCSLL, bool vPISSpecified, bool vCOFINSSpecified, bool vIRSpecified, bool vINSSSpecified, bool vCSLLSpecified)
        {
            VPIS = vPIS;
            VCOFINS = vCOFINS;
            VIR = vIR;
            VINSS = vINSS;
            VCSLL = vCSLL;
            VPISSpecified = vPISSpecified;
            VCOFINSSpecified = vCOFINSSpecified;
            VIRSpecified = vIRSpecified;
            VINSSSpecified = vINSSSpecified;
            VCSLLSpecified = vCSLLSpecified;
        }

        public decimal? VPIS { get; private set; }

        public decimal? VCOFINS { get; private set; }

        public decimal? VIR { get; private set; }

        public decimal? VINSS { get; private set; }

        public decimal? VCSLL { get; private set; }

        public bool VPISSpecified {get; private set;}
        
        public bool VCOFINSSpecified {get; private set;}
        
        public bool VIRSpecified {get; private set;}
        
        public bool VINSSSpecified {get; private set;}
        
        public bool VCSLLSpecified {get; private set;}
        
    }
}