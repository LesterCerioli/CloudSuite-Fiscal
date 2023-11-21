using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.ViewModels
{
    public class DistrictViewModel
    {
        [Key]
        public Guid Id { get; private set; }

        public State State { get; set; }

        public Guid StateId { get; private set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(450)]
        public string? Name { get; private set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public string? Type { get; private set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(100)]
        public string? Location { get; private set; }
    }
}
