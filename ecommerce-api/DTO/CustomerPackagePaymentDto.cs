using System;

namespace ecommerce_api.DTO
{
    public class CustomerPackagePaymentDto
    {
        public CustomerPackagePaymentDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int User_ID { get; set; }
        public int Customer_package_ID { get; set; }
        public string Payment_method { get; set; }
        public string Payment_detail { get; set; }
        public int Approval { get; set; }
        public int Offline_payment { get; set; }
        public string Reciept { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}