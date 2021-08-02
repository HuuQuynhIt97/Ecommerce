using System;

namespace ecommerce_api.DTO
{
    public class ConversationDto
    {
        public ConversationDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Sender_ID { get; set; }
        public int Receiver_ID { get; set; }
        public string Title { get; set; }
        public int Sender_viewed { get; set; }
        public int Receiver_viewed { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}