using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfyaSoftEmail.Data;
using AfyaSoftEmail.Models;
using Microsoft.EntityFrameworkCore;

namespace AfyaSoftEmail.Services
{
    public class EmailService
    {
        private DbContextOptions<AppDbContext> options;

        public EmailService()
        {
        }

        public EmailService(DbContextOptions<AppDbContext> options)
        {
            this.options = options;
        }

        public async Task SaveData(EmailLoggers emailLoggers)
        {
            var _context = new AppDbContext(options);
            _context.EmailLoggers.Add(emailLoggers);
            await _context.SaveChangesAsync();
        }
    }
}