using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace EfDbCarService
{
    public class CarServiceDbContext: DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Service> Services { get; set; }

        public CarServiceDbContext(DbContextOptions<CarServiceDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
