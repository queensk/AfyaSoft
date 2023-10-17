using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfyaSoftEmail.Models;
using Microsoft.EntityFrameworkCore;

namespace AfyaSoftEmail.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }
         public DbSet<EmailLoggers> EmailLoggers { get; set; } 
    }
}