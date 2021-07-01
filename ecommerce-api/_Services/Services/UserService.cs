﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using ecommerce_api._Repositories.Interface;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using ecommerce_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_api._Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository,
            IMapper mapper,
            MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public bool IsValidUserCredentials(string userName, string password)
        {
            _logger.LogInformation($"Validating user [{userName}]");
            if (string.IsNullOrWhiteSpace(userName))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            var user = _context.Users.Where(x => x.IsShow == true).FirstOrDefault(x => x.EmployeeID.ToLower() == userName.ToLower());
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return false;

            return true;
        }
        public async Task<bool> Add(UserDto model)
        {
            var item = await _userRepository.FindAll().FirstOrDefaultAsync(x => x.Username.ToLower().Equals(model.Username.ToLower()) || x.Email.Equals(model.Email));
            if (item == null)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    
                };
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                _userRepository.Add(user);
                try
                {
                    await _userRepository.SaveAll();
                   
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
        public Task<bool> Delete(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            return await _userRepository.FindAll().ProjectTo<UserDto>(_configMapper).ToListAsync();

        }

        public async Task<List<UserDto>> GetUserRoleByID(int userID)
        {
            return await _userRepository.FindAll().Where(x => x.ID == userID).ProjectTo<UserDto>(_configMapper).ToListAsync();
        }

        public UserDto GetById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<UserDto>> GetWithPaginations(PaginationParams param)
        {
            throw new NotImplementedException();
        }

        public async Task<object> MapUserRole(int userid, int roleID)
        {
            var item = await _userRepository.FindAll().Where(x => x.ID == userid).ToListAsync();
            if (item.Count == 0)
            {
                try
                {
                    _userRepository.Add(new User
                    {
                        ID = userid,
                    });

                    return new
                    {
                        status = await _userRepository.SaveAll(),
                        message = "Mapping Successfully!"
                    };
                }
                catch (Exception)
                {
                    return new
                    {
                        status = false,
                        message = "Failed on save!"
                    };
                }
            }
            else
            {

                try
                {
                    _userRepository.RemoveMultiple(item);
                    await _userRepository.SaveAll();

                    _userRepository.Add(new User
                    {
                        ID = userid,
                    });

                    
                    return new
                    {
                        status = await _userRepository.SaveAll(),
                        message = "Mapping Successfully!"
                    };
                }
                catch (Exception)
                {
                    return new
                    {
                        status = false,
                        message = "Failed on save!"
                    };
                }
            }
        }

        public async Task<object> MappingUserRole(UserDto userRoleDto)
        {
            var item = await _userRepository.FindAll().FirstOrDefaultAsync(x => x.ID == userRoleDto.ID && x.ID == userRoleDto.ID);
            if (item == null)
            {
                _userRepository.Add(new User
                {
                    ID = userRoleDto.ID,
                });
                try
                {
                    await _userRepository.SaveAll();
                    return new
                    {
                        status = true,
                        message = "Mapping Successfully!"
                    };
                }
                catch (Exception)
                {
                    return new
                    {
                        status = false,
                        message = "Failed on save!"
                    };
                }
            }
            else
            {

                return new
                {
                    status = false,
                    message = "The User belonged with other user!"
                };
            }
        }

        public async Task<object> RemoveUserRole(UserDto userRoleDto)
        {
            var item = await _userRepository.FindAll().FirstOrDefaultAsync(x => x.ID == userRoleDto.ID && x.ID == userRoleDto.ID);
            if (item != null)
            {
                _userRepository.Remove(item);
                try
                {
                    await _userRepository.SaveAll();
                    return new
                    {
                        status = true,
                        message = "Delete Successfully!"
                    };
                }
                catch (Exception)
                {
                    return new
                    {
                        status = false,
                        message = "Failed on delete!"
                    };
                }
            }
            else
            {

                return new
                {
                    status = false,
                    message = ""
                };
            }
        }

        public Task<PagedList<UserDto>> Search(PaginationParams param, object text)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(UserDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<object> GetRoleByID(int userid)
        {
            var model = await _userRepository.FindAll().FirstOrDefaultAsync(x => x.ID == userid);

            return model.ID;
        }

        public async Task<bool> Lock(UserDto userRoleDto)
        {
            var model = await _userRepository.FindAll().FirstOrDefaultAsync(x => x.ID == userRoleDto.ID );
            if (model != null)
            {
                _userRepository.Update(model);

                return await _userRepository.SaveAll();
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> IsLock(UserDto userRoleDto)
        {
            var model = await _userRepository.FindAll().FirstOrDefaultAsync(x => x.ID == userRoleDto.ID );
            if (model != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<List<UserDto>> GetUserRoleByUserID(int userID)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetRoleByUserID(int userid)
        {
            throw new NotImplementedException();
        }
    }
}
