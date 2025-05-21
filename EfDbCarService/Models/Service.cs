using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbCarService
{
    public class Service
    {
        [Key]
        public int ServiceID { get; set; }
        public string ServiceName {  get; set; }
        public decimal Price { get; set; }

        public string? ImagePath { get; set; }
    }
}
