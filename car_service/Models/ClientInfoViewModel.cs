using System.Dynamic;

namespace car_service.Models
{
    public class ClientInfoViewModel
    {
        public int ClientId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        public List<CarInfo> Cars { get; set; } = new();
        public List<OrderInfo> Orders { get; set; } = new();
    }
}
