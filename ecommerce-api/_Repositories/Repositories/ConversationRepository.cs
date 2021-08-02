using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class ConversationRepository : ECRepository<Conversation>, IConversationRepository
    {
        public ConversationRepository(DataContext context) : base(context)
        {
        }
    }
}