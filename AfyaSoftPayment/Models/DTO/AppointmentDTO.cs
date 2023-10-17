using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AfyaSoftPayment.Models.DTO
{
    public class AppointmentDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid DoctorId { get; set; }
        public DoctorDTO? Doctor { get; set; }
        public Guid PatientId { get; set; }
        public PatientDTO? Patient { get; set; }
        public List<SymptomDTO>  symptoms { get; set; } = new List<SymptomDTO>();
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } 
        
    }
}