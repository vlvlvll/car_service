
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbCarService
{
    public interface IServicesRepository
    {
            void Add(Service service);
            void Delete(Service service);
            List<Service> GetAll();
            Service TryGetById(int id);
           // public void Update(Service service, string title, string description);
        
    }
}
