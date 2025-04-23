using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace car_service
{
    public class Part
    {
        [Key]
        public int Id { get; set; }
        public string PartName {  get; set; }
        public decimal Price { get; set; }
    }
}
