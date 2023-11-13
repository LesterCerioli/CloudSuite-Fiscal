using CloudSuite.Modules.Common.Enums;

namespace CloudSuite.Modules.Domain.Models
{
    public class Note
    {
        
        public NFSeSimNao IncentivadorCultural { get; set; }

        public NFSeSimNao Producao { get; set; }

        public SituacaoNFSeRps Situacao { get; set; }

        public TipoLocalServico LocalServico { get; set; }

        public int? NumeroLote { get; set; }

        //Protocolo
        public string? Protocol { get; private set; }

        public DateTime? Competence { get; private set; }

        public string? Anotherinformation { get; private set; }

        public string? TaxDescription { get; private set; }
        
        //Descricao Codigo Tributacao Municipio
        public string? DescrCodeTaxMunicip { get; private set; }
        

        public decimal? CreditValue { get; private set; }

        public EmissionType EmissionType { get; private set; }

        public GlobalEnterpriseType GlobalEnterpriseType { get; private     set; }

        public TaxationType TaxationType { get; private set; }

        public string? Signature { get; private set; }

        //public DFeSignature Signature { get; set; }

        //public string? XmlOriginal { get; private set; }

        
    }
}