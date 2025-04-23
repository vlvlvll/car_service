using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbCarService
{
    public class ServicesDbRepository : IServicesRepository
    {
        private readonly CarServiceDbContext dbcontext;

        public ServicesDbRepository(CarServiceDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public void Add(Service service)
        {
            throw new NotImplementedException();
        }

        public void Delete(Service service)
        {
            throw new NotImplementedException();
        }

        public List<Service> GetAll()
        {
            return dbcontext.Services.ToList();
        }

        public Service TryGetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
