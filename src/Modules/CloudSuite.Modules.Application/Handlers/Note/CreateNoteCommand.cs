using CloudSuite.Modules.Application.Handlers.Note.Responses;
using CloudSuite.Modules.Common.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteEntity = CloudSuite.Modules.Domain.Models.Note;

namespace CloudSuite.Modules.Application.Handlers.Note
{
    public class CreateNoteCommand : IRequest<CreateNoteResponse>
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

        public GlobalEnterpriseType GlobalEnterpriseType { get; private set; }

        public TaxationType TaxationType { get; private set; }

        public string? Signature { get; private set; }

        //public DFeSignature Signature { get; set; }

        //public string? XmlOriginal { get; private set; }


        public NoteEntity GetEntity()
        {
            return new NoteEntity(
                
                );
        }
    }
}
