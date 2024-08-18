using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XuongMay_BE.Data;
using XuongMay_BE.Models;

namespace XuongMay_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _context;

        public UsersController(MyDbContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var dsUsers = _context.Users.ToList();
            return Ok(dsUsers);
        }


        [HttpPost]
        public  IActionResult CreateUser([FromForm] RegisterRequest model)
        {
            try
            {
                //Gán giá trị nhập vào từng thuộc tính của order
                var newbie = new User()
                {
                    UserName = model.UserName,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword,
                };
                var dsUser = _context.Users.Where(x => x.UserName == newbie.UserName).FirstOrDefault();
                //Kiểm tra có tồn tại UserName này trong dsUser chưa
                if (dsUser == null)
                {
                    //Kiểm tra Password và ConfirmPassword có giống không
                    if(newbie.Password == newbie.ConfirmPassword)
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
        
    }
}
