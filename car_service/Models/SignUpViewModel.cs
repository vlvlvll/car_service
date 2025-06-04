using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace car_service.Models
{
    public class SignUpViewModel
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }

        
        [Required]
        public string FullName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        
        [Required]
        public string Brand { get; set; }

        [Required]
        public string Version { get; set; }

        [Required]
        public string VIN { get; set; }
    }

}
