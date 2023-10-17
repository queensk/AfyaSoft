using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AfyaSoftPayment.Models.DTO
{
    [NotMapped]
    public class DoctorDTO
    {
        public Guid Id {get; set;}
        public string Name {get; set;}
        public string Specialization {get; set;}
    }
}