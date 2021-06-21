using System.Threading.Tasks;
using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ecommerce_api.DTO;
using System.Collections.Generic;

namespace ecommerce_api._Repositories.Repositories
{
    public class UserRepository : ECRepository<User>, IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        //Login khi them repo
    }
}