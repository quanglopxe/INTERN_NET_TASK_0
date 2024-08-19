using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XuongMay_BE.Data;
using XuongMay_BE.Models;
using RegisterRequest = XuongMay_BE.Models.RegisterRequest;
using LoginRequest = XuongMay_BE.Models.LoginRequest;

namespace XuongMay_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly AppSettings _appSettings;

        public UsersController(MyDbContext context, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var dsUsers = _context.Users.ToList();
            return Ok(dsUsers);
        }


        [HttpPost("register")]
        public IActionResult CreateUser([FromForm] RegisterRequest model)
        {
            try
            {
                //Gán giá trị nhập vào từng thuộc tính của order
                var newbie = new User()
                {
                    Name = model.Name,
                    UserName = model.UserName,
                    Password = model.Password,
                    Roles = model.Roles,
                };
                var dsUser = _context.Users.Where(x => x.UserName == newbie.UserName).FirstOrDefault();
                //Kiểm tra có tồn tại UserName này trong dsUser chưa
                if (dsUser == null)
                {
                    //Kiểm tra Password và ConfirmPassword có giống không
                    if (newbie.Password == model.ConfirmPassword)
                    {
                        _context.Add(newbie);
                        _context.SaveChanges();
                        return Ok(newbie);
                    }
                    else
                    {
                        return BadRequest("Password và ConfirmPassword không giống nhau");
                    }
                }
                else
                {
                    return BadRequest("User trùng");
                }
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPost("login")]
        public IActionResult Login([FromForm] LoginRequest model)
        {
            var user = _context.Users.Where(x => x.UserName == model.Username && x.Password == model.Password).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("Sai tên đăng nhập hoặc mật khẩu");                
            }

            //cấp token
            return Ok(GenerateToken(user));
        }

        private string GenerateToken(User userInfo)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userInfo.Name),
                    new Claim("Username", userInfo.UserName),
                    new Claim("ID", userInfo.UserID.ToString()),
                    //role
                    new Claim(ClaimTypes.Role, userInfo.Roles),

                    new Claim("TokenID", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}