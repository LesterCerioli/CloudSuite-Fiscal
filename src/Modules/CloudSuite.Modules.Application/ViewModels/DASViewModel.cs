using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.ViewModels
{
    public class DASViewModel
    {
        [Key]
        public Guid Id { get; private set; }

        public string? ReferenceMonth { get; private set; }

        public DateTime? DueDate { get; private set; }

        public string? ReferenceYear { get; private set; }

        public string? PaymentValue { get; private set; }

        public string? DocumentNumber { get; private set; }

        public string? BarCode { get; private set; }

    }
}
