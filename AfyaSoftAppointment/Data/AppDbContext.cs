using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfyaSoftAppointment.Model;
using Microsoft.EntityFrameworkCore;

namespace AfyaSoftAppointment.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Appointment> Appointments  { get; set; }
    }
}