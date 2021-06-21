using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_api._Services.Interface
{
   public interface IUserService : IECService<UserDto>
    {
        Task<object> MappingUserRole(UserDto userRoleDto);
        Task<object> RemoveUserRole(UserDto userRoleDto);
        Task<List<UserDto>> GetUserRoleByUserID(int userID);
        Task<object> GetRoleByUserID(int userid);
        Task<object> MapUserRole(int userID, int roleID);
        Task<bool> Lock(UserDto userRoleDto);
        Task<bool> IsLock(UserDto userRoleDto);
    }
}
