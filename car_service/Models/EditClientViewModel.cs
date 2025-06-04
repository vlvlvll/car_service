namespace car_service.Models
{
    public class EditClientViewModel
    {
        public int ClientId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Version { get; set; }
        public string VIN { get; set; }

        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
