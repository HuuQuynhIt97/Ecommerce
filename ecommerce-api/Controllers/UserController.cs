using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ecommerce_api.Helpers;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserDetails([FromQuery]PaginationParams param)
        {
            var userDetails = await _userService.GetWithPaginations(param);
            Response.AddPagination(userDetails.CurrentPage,userDetails.PageSize,userDetails.TotalCount,userDetails.TotalPages);
            return Ok(userDetails);
        }

        // [HttpGet(Name = "GetUserDetails")]
        // public async Task<IActionResult> GetAll()
        // {
        //     var userDetails = await _userService.GetAllAsync();
        //     return Ok(userDetails);
        // }
        // [HttpGet("{id}",Name = "GetUserSingle")]
        
        // public IActionResult Single(int id)
        // {
        //     var userDetails =  _userService.GetById(id);
        //     return Ok(userDetails);
        // }
        [HttpGet("{text}")]
        public async Task<IActionResult> Search([FromQuery]PaginationParams param, string text)
        {
            var lists = await _userService.Search(param, text);
            Response.AddPagination(lists.CurrentPage, lists.PageSize, lists.TotalCount, lists.TotalPages);
            return Ok(lists);
        }
       
        [HttpPost]
        public async Task<IActionResult> Create(UserDto create)
        {
            // if (_userService.GetById(create.ID).ID > 0)
            //     return BadRequest("UserDetail ID already exists!");
            if (await _userService.Add(create))
            {
                return NoContent();
            }
            throw new Exception("Creating the user failed on save");
        }
        
        [HttpPut]
        public async Task<IActionResult> Update(UserDto update)
        {
            if (await _userService.Update(update))
                return NoContent();
            return BadRequest($"Updating user detail {update.ID} failed on save");
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _userService.Delete(id))
                return NoContent();
            throw new Exception("Error deleting the user detail");
        }
    }
}