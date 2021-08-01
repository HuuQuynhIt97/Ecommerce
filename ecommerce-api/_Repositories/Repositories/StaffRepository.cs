using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class StaffRepository : ECRepository<Staff>, IStaffRepository
    {
        public StaffRepository(DataContext context) : base(context)
        {
        }
    }
}