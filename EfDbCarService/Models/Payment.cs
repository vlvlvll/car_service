using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbCarService
{
    public class Payment
    {
        [Key]
        public int Id {  get; set; }
        public string PaymentMethod {  get; set; }

        public int OrderId {  get; set; }
        public Order Order { get; set; }
    }
}
