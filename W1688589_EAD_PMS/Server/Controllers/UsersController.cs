using W1688589_EAD_PMS.Server.Repository.UserRepos;
using W1688589_EAD_PMS.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class UsersController : ControllerBase
    {
        private IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        [Authorize(Roles = "Admin,SubAdmin")]
        public async Task<IActionResult> AddNewMember([FromBody] UserMV users)
        {
            try
            {
                string message = await _repository.CreateNewUser(users);
                return Ok(message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occured, during Adding Member");
            }
        }

      

        [HttpPost]
        [Authorize(Roles = "Admin,SubAdmin")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserMV memeber)
        {
            try
            {
                string message = await _repository.UpdateUser(memeber);
                return Ok(message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occured, while updating Member");
            }
        }

        [HttpGet("{cid}")]
        [Authorize(Roles = "Admin,SubAdmin,Normal")]
        public async Task<ActionResult> GetAllWorkspaceUser(int cid)
        {
            try
            {
                return Ok(await _repository.GetAllUserByWorkspaceID(cid));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occured, while retrieving the data");
            }
        }
        [HttpGet("{uid}/{role}")]
        [Authorize(Roles = "Admin,SubAdmin")]
        public async Task<ActionResult> ChangeUserRole(string uid, string role)
        {
            try
            {
                var result = await _repository.ChangeUserRole(uid, role);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occured during updating role");
            }
        }
        [HttpGet("{pid}")]
        [Authorize(Roles = "Admin,SubAdmin,Normal")]
        public async Task<ActionResult> GetAllUserByPid(int pid)
        {
            try
            {
                var result = await _repository.GetAllUserByPid(pid);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occured during retrieving Data");
            }
        }
        [HttpGet("{uid}")]
        [Authorize(Roles = "Admin,SubAdmin")]
        public async Task<ActionResult> GetUserById(string uid)
        {
            try
            {
                var result = await _repository.GetUserById(uid);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occured during retrieving the data");
            }
        }
        [HttpGet("{uid}")]
        [Authorize(Roles = "Admin,SubAdmin")]
        public async Task<ActionResult> DeleteUser(string uid)
        {
            try
            {
                var result = await _repository.DeleteUser(uid);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occured during deleting");
            }
        }
        [HttpGet("{cid:int}/{email}")]
        [Authorize(Roles = "Admin,SubAdmin,Normal")]
        public async Task<ActionResult> SearchUsers(int cid, string email)
        {
            try
            {
                return Ok(await _repository.SearchUsers(cid, email));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occured, while retrieving the data");
            }
        }
    }
}
