using System;

namespace ecommerce_api.DTO
{
    public class TicketReplyDto
    {
        public TicketReplyDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Ticket_ID { get; set; }
        public int User_ID { get; set; }
        public string Reply { get; set; }
        public string Files { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}