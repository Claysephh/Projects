using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using Capstone.DAO;
using Capstone.Models;
using System.Collections.Generic;



namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserDao userDao;
        public UserController(IUserDao _userDao) 
        {
            userDao = _userDao;
        }
        [HttpGet("/getusers")]
        [Authorize]
        public ActionResult<List<ReturnUser>> GetUsers()
        {
            try
            {
                List<ReturnUser> users = new List<ReturnUser>();
                users = userDao.GetRegularUsers();
                return users;
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPut("/editprofile")]
        [Authorize(Roles = "user,owner,admin")]
        public IActionResult EditProfile(RegisterUser user)
        {
            try
            {
                user.UserId = Convert.ToInt32(User.FindFirst("sub")?.Value);
                if (user.Password == null || user.Username == null)
                {
                    return BadRequest(new { message = "That is not a valid password or username." });
                }
                if (user.ConfirmPassword == user.Password)
                {
                    RegisterUser result = userDao.Edit(user);
                }
                else
                {
                    return BadRequest(new { message = "The password and the confirm password do not match" });
                }
                return Accepted();
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPut("/edituser")]
        [Authorize(Roles = "admin")]
        public IActionResult EditUser(RegisterUser user)
        {
            try
            {
                if (user.Password == null || user.Username == null)
                {
                    return BadRequest(new { message = "That is not a valid password or username." });
                }
                if (user.ConfirmPassword == user.Password)
                {
                    RegisterUser result = userDao.Edit(user);
                }
                else
                {
                    return BadRequest(new { message = "The password and the confirm password do not match" });
                }
                return Accepted();
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpDelete("/deleteuser/{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteUsers(int id)
        {
            User existingUser = userDao.GetUser(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            bool result = userDao.Disable(id);
            if (result)
            {
                return NoContent();
            }
            return StatusCode(500);
        }
        //[HttpGet("wineryAuth")]
        //public ActionResult<List<int>> GetWineriesById()
        //{
        //    int id = Convert.ToInt32(User.FindFirst("sub")?.Value);
        //    List<int> wineryIds = new List<int>();
        //    try
        //    {
        //        wineryIds = userDao.GetWineryIdsFromOwnerId(id);
        //    }
        //    catch(Exception ex)
        //    {
        //        return StatusCode(500, new { message = ex.Message });
        //    }
        //    return Ok(wineryIds);
        //}
    }
}
