using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AfyaSoft.Models
{
    public class Appointment
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Preferred appointment date is required")]
        public DateTime? PreferredAppointmentDate { get; set; }

        [Required(ErrorMessage = "Reason for visit is required")]
        public string ReasonForVisit { get; set; }

        [Required(ErrorMessage = "Hospital selection is required")]
        public string SelectedHospital { get; set; }

        [Required(ErrorMessage = "Doctor selection is required")]
        public string SelectedDoctor { get; set; }

        [Required(ErrorMessage = "Existing patient selection is required")]
        public bool? IsExistingPatient { get; set; }
    }
}