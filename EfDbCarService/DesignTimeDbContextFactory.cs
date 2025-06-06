using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbCarService
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CarServiceDbContext>
    {
        public CarServiceDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CarServiceDbContext>();
            optionsBuilder.UseMySql(
                "server=localhost;port=3306;user=root;password=Aa123456;database=car_service",
                new MySqlServerVersion(new Version(8, 0, 32))
            );

            return new CarServiceDbContext(optionsBuilder.Options);
        }
    }
}
