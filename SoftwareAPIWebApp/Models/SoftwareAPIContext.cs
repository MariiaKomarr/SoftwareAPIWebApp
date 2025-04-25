using Microsoft.EntityFrameworkCore;

namespace SoftwareAPIWebApp.Models
{
    public class SoftwareAPIContext : DbContext
    {
        public SoftwareAPIContext(DbContextOptions<SoftwareAPIContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<SoftwareType> SoftwareTypes { get; set; }
        public DbSet<Software> Softwares { get; set; }
        public DbSet<Installation> Installations { get; set; }
    }
}
