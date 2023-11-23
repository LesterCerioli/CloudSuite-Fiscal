using CloudSuite.Modules.Application.Handlers.City.Responses;
using CloudSuite.Modules.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityEntity = CloudSuite.Modules.Domain.Models.City;
using StateEntity = CloudSuite.Modules.Domain.Models.State;

namespace CloudSuite.Modules.Application.Handlers.City
{
    public class CreateCityCommand : IRequest<CreateCityResponse>
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [MaxLength(100)]
        public string? CityName { get; private set; }

        public StateEntity State { get; private set; }

        public Guid StateId { get; private set; }

        public CreateCityCommand()
        {
            Id = Guid.NewGuid();
        }

        public CityEntity GetEntity()
        {
            return new CityEntity(
                this.StateId,
                this.CityName,
                this.State
                );
        }
    }
}
