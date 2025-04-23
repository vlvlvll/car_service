using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace car_service
{
    public class Mechanic
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public string PhoneNumber { get; set; }
    }
}
