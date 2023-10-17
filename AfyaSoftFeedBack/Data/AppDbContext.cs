using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfyaSoftFeedBack.Model;
using Microsoft.EntityFrameworkCore;

namespace AfyaSoftFeedBack.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Feedback> FeedBack  { get; set; }
    }
}