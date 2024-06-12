using Exam.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Shared.DTOs
{
    public class PersonDeleteDTO
    {

        [Required]
        public int Id { get; set; }

    }
}
