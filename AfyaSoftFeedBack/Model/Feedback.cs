using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AfyaSoftFeedBack.Model
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public string FeedbackText { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt  { get; set; }
        public DateTime UpdatedAt { get; set;  }
    }
}