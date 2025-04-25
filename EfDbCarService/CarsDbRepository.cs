using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbCarService
{
    public class CarsDbRepository : ICarsRepository
    {
        private readonly CarServiceDbContext dbcontext;

        public CarsDbRepository(CarServiceDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public void Add(Car car)
        {
            throw new NotImplementedException();
        }

        public void Delete(Car car)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return dbcontext.Cars.ToList();
        }

        public Car TryGetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
