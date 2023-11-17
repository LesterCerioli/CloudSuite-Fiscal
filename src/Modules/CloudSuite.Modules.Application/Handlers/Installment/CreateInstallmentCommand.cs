using CloudSuite.Modules.Application.Handlers.Installment.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstallmentEntity = CloudSuite.Modules.Domain.Models.Installment;

namespace CloudSuite.Modules.Application.Handlers.Installment
{
    public class CreateInstallmentCommand : IRequest<CreateInstallmentResponse>
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Preenchimento Obrigatório")]
        public DateTime? DueDate { get; private set; }

        [Required(ErrorMessage = "Preenchimento Obrigatório")]
        public decimal? Value { get; private set; }

        [Required(ErrorMessage = "Preenchimento Obrigatório")]
        public string? Description { get; private set; }


        public InstallmentEntity GetEntity()
        {
            return new InstallmentEntity(
                this.Id = Guid.NewGuid(),
                this.DueDate,
                this.Value,
                this.Description
                );
        }
    }
}
