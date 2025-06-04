using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace car_service
{
    public class Car
    {
        [Key]
        public int CarId {  get; set; }
        public string Brand { get; set; }
        public string Version { get; set; }
        public string VIN { get; set; }

        public Client Client{ get; set; }
        public int ClientId { get; set; }

        public List<Order> Orders { get; set; } = new();
    }
}
