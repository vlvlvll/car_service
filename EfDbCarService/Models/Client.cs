using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbCarService
{
    public class Client
    {
        [Key]
        public int ClientId {  get; set; }
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public List<Car> Cars { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
    }
}
