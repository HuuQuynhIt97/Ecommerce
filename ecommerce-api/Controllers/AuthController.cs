using System.Threading.Tasks;
using ecommerce_api.DTO;
using ecommerce_api.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using ecommerce_api.Helpers;
using ecommerce_api._Services.Interface;

namespace ecommerce_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IAuthRepository _repo;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public AuthController(ILogger<AuthController> logger, IJwtAuthManager jwtAuthManager, IAuthRepository repo, IConfiguration config, IMapper mapper)
        {
            _mapper = mapper;
            _config = config;
            _logger = logger;
            _jwtAuthManager = jwtAuthManager;
            _repo = repo;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!_userService.IsValidUserCredentials(request.UserName, request.Password))
            {
                return Unauthorized();
            }
            var user = await _userService.GetUser(request.UserName, request.Password);
            var claims = new[]
            {
               new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Name, user.EmployeeID),
                new Claim(ClaimTypeEnum.OCID.ToString(), user.OCID.ToString(),ClaimTypeEnum.OCID.ToString()),
                new Claim(ClaimTypeEnum.Role.ToString(), user.RoleID.ToString(),ClaimTypeEnum.Role.ToString())
            };

            var jwtResult =  _jwtAuthManager.GenerateTokens(request.UserName, claims, DateTime.Now);

           await  _jwtAuthManager.AddRefreshToken(jwtResult.RefreshToken);
            // Xóa những token đã hết hạn khỏi db
           await _jwtAuthManager.RemoveExpiredRefreshTokens(DateTime.Now);
            _logger.LogInformation($"User [{request.UserName}] logged in the system.");
            var userprofile = new UserProfileDto()
            {
                User = new UserForReturnLogin
                {
                    Username = user.Username,
                    Role = user.RoleID,
                    ID = user.ID,
                    OCLevel = user.LevelOC,
                    IsLeader = user.isLeader,
                    image = user.ImageBase64,
                    Systems = user.UserSystems.Where(x => x.Status == true).Select(x => x.SystemID).ToList()
                }
            };
            return Ok(new
            {
                loginResult = new LoginResult
                {
                    UserName = request.UserName,
                    Role = user.Role.Name,
                    ID = user.ID,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString
                },
                user = userprofile
            });
        }

        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var userName = User.Identity.Name;
                _logger.LogInformation($"User [{userName}] is trying to refresh JWT token.");

                if (string.IsNullOrWhiteSpace(request.RefreshToken))
                {
                    return Unauthorized();
                }

                var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
                var jwtResult = _jwtAuthManager.Refresh(request.RefreshToken, accessToken, DateTime.Now);
                _logger.LogInformation($"User [{userName}] has refreshed JWT token.");
                return Ok(new LoginResult
                {
                    UserName = userName,
                    Role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString
                });
            }
            catch (SecurityTokenException e)
            {
                return Unauthorized(e.Message); // return 401 so that the client side can redirect the user to login page
            }
        }

    }
    public class LoginRequest
    {
        [Required]
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }

    public class LoginResult
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("originalUserName")]
        public string OriginalUserName { get; set; }

        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }

    public class RefreshTokenRequest
    {
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }

    public class ImpersonationRequest
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }
    }
}