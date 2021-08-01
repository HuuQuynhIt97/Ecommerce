using System;

namespace ecommerce_api.DTO
{
    public class AddressesDto
    {
        public AddressesDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int User_ID { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Postal_code { get; set; }
        public string Phone { get; set; }
        public bool Set_default { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}