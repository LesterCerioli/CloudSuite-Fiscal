using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Responses;
using CloudSuite.Modules.Application.Handlers.District.Responses;
using CloudSuite.Modules.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistrictEntity = CloudSuite.Modules.Domain.Models.District;
using StateEntity = CloudSuite.Modules.Domain.Models.State;

namespace CloudSuite.Modules.Application.Handlers.District
{
    public class CreateDistrictCommand : IRequest<CreateDistrictResponse>
    {
        public Guid Id { get; set; }

        public StateEntity State { get; set; }

        public Guid StateId { get; private set; }

        public string? Name { get; private set; }

        public string? Type { get; private set; }

        public string? Location { get; private set; }


        public DistrictEntity GetEntity()
        {
            return new DistrictEntity(
                Guid.NewGuid(),
                this.Name,
                this.Type,
                this.Location
                );
        }
    }
}
