using Microsoft.EntityFrameworkCore;
using Shop_Models.Entities;

namespace Shop_API.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<Ram> Rams { get; set; }
        public virtual DbSet<Cpu> Cpus { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<HardDrive> HardDrives { get; set; }
        public virtual DbSet<Screen> Screens { get; set; }
        public virtual DbSet<CardVGA> CardVGAs { get; set; }

    }
}
