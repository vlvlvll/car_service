using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbCarService
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }

        public int ClientId {  get; set; }
        public Client Client { get; set; }
        public int CarId {  get; set; }
        public Car Car { get; set; }

        public List<Payment> Payments { get; set; } = new();
    }
}
