using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AfyaSoftAppointment.Model
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public Guid PatientId { get; set; }
        public Patient? Patient { get; set; }
        public List<Symptom>  symptoms { get; set; } = new List<Symptom>();
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } 
        
    }
}