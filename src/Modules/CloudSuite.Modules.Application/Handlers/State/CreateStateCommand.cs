using CloudSuite.Modules.Application.Handlers.Note.Responses;
using CloudSuite.Modules.Application.Handlers.State.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountryEntity = CloudSuite.Modules.Domain.Models.Country;
using StateEntity = CloudSuite.Modules.Domain.Models.State;

namespace CloudSuite.Modules.Application.Handlers.State
{
    public class CreateStateCommand : IRequest<CreateStateResponse>
    {
        public Guid Id { get; private set; }

        public string? StateName { get; set; }

        public string? UF { get; set; }

        public CountryEntity Country { get; set; }

        public Guid CountryId { get; set; }

        public CreateStateCommand()
        {
            Id = Guid.NewGuid();
        }
        public StateEntity GetEntity() 
        {
            return new StateEntity(
                this.Id = Guid.NewGuid(),
                this.UF,
                this.StateName,
                this.Country,
                this.CountryId
                );
        }
    }
}
