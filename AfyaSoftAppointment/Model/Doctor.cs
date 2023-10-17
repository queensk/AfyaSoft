using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AfyaSoftAppointment.Model
{
    [NotMapped]
    public class Doctor
    {
        public Guid Id {get; set;}
        public string Name {get; set;}
        public string Specialization {get; set;}
    }
}