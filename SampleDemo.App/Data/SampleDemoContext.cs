using SampleDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace SampleDemo.Data
{
    public class SampleDemoContext : DbContext
    {
        public SampleDemoContext(DbContextOptions<SampleDemoContext> opt) : base(opt)
        {
            
        }

        public DbSet<Person> Persons { get; set; }

    }
}