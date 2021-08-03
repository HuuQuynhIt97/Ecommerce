using System;

namespace ecommerce_api.DTO
{
    public class MessageDto
    {
        public MessageDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Conversation_ID { get; set; }
        public int User_ID { get; set; }
        public string Message { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}