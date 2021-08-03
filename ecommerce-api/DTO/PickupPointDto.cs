using System;

namespace ecommerce_api.DTO
{
    public class PickupPointDto
    {
        public PickupPointDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Staff_ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Pick_up_status { get; set; }
        public int Cash_on_pickup_status { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}