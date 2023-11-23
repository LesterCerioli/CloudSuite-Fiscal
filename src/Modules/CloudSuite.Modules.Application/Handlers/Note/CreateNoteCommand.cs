using CloudSuite.Modules.Application.Handlers.Note.Responses;
using CloudSuite.Modules.Common.Enums;
using CloudSuite.Modules.Common.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteEntity = CloudSuite.Modules.Domain.Models.Note;
using TomadorServicoEntity = CloudSuite.Modules.Domain.Models.TomadorServico;
using AddressEntity = CloudSuite.Modules.Domain.Models.Address;
using CountryEntity = CloudSuite.Modules.Domain.Models.Country;
using DistrictEntity = CloudSuite.Modules.Domain.Models.District;
using PrestadorEntity = CloudSuite.Modules.Domain.Models.Prestador;


namespace CloudSuite.Modules.Application.Handlers.Note
{
    public class CreateNoteCommand : IRequest<CreateNoteResponse>
    {
        public Guid Id { get; private set; }

        public TomadorServicoEntity TomadorServico { get; private set; }

        public AddressEntity Address { get; private set; }

        public CountryEntity Country { get; private set; }

        public DistrictEntity District { get; private set; }

        public PrestadorEntity Prestador { get; private set; }

        public Cnpj Cnpj { get; private set; }

        public string? NoteNumber { get; private set; }

        public DateTime? EmissionDate { get; private set; }

        public decimal Value { get; private set; }

        public CreateNoteCommand()
        {
            Id = Guid.NewGuid();
        }
        public NoteEntity GetEntity()
        {
            return new NoteEntity(
                this.TomadorServico,
                this.Address,
                this.Country,
                this.District,
                this.Prestador,
                this.Cnpj,
                this.NoteNumber,
                this.Value
                );
        }
    }
}
