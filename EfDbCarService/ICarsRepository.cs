using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbCarService
{
    public interface ICarsRepository
    {
        void Add(Car car);
        void Delete(Car car);
        List<Car> GetAll();
        Car TryGetById(int id);
    }
}
