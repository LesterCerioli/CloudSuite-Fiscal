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
        public Guid Id { get; set; }

        public string? ReferenceMonth { get; private set; }

        public DateTime DueDate { get; private set; }

        public string? ReferenceYear { get; private set; }

        public string? PaymentValue { get; private set; }

        public string? DocumentNumber { get; private set; }

        public string? BarCode { get; private set; }


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
