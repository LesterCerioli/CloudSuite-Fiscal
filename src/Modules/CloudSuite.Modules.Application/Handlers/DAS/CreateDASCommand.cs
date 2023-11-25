using CloudSuite.Modules.Application.Handlers.Darf.Responses;
using CloudSuite.Modules.Application.Handlers.DAS.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DASEntity = CloudSuite.Modules.Domain.Models.DAS;

namespace CloudSuite.Modules.Application.Handlers.DAS
{
    public class CreateDASCommand : IRequest<CreateDASResponse>
    {
        public Guid Id { get; private set; }

        public string ReferenceMonth { get; set; }

        public DateTime DueDate { get; set; }

        public string? ReferenceYear { get; set; }

        public string? PaymentValue { get; set; }

        public string? DocumentNumber { get; set; }

        public string? BarCode { get; set; }

        public CreateDASCommand()
        {
            Id = Guid.NewGuid();
        }

        public DASEntity GetEntity()
        {
            return new DASEntity(
                this.ReferenceMonth,
                this.DueDate,
                this.ReferenceYear,
                this.PaymentValue,
                this.DocumentNumber,
                this.BarCode
                );
        }
    }
}
