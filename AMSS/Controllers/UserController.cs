using AMSS.Enums;
using AMSS.Models;
using AMSS.Repository.IRepository;
using AMSS.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AMSS.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        protected APIResponse _response;
        public UserController(IUserRepository userRepository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _response = new APIResponse();
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        [HttpGet("getAll")]
        [Authorize(Roles = nameof(Role.ADMIN))]
        public async Task<ActionResult<APIResponse>> GetAllUsers()
        {
            try
            {
                List<ApplicationUser> lstUsers = await _userRepository.GetAllAsync();

                if (lstUsers == null)
                {
                    _response.Result = new List<ApplicationUser>();
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Something wrong when get all users");
                    return BadRequest(_response);
                }

                foreach (var user in lstUsers)
                {
                    user.Role = Enum.Parse<Role>(_userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault());
                }

                _response.Result = lstUsers;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Result = new List<ApplicationUser>();
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add(ex.Message);
            }
            return BadRequest(_response);
        }


        [HttpPost("lockUnlock/{id}")]
        public async Task<ActionResult<APIResponse>> LockUnlock(string? id)
        {
            try
            {
                var userFromDb = await _userRepository.GetAsync(u => u.Id == id);
                if (userFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Errors while lock/unlock this user");
                    return BadRequest(_response);
                }
                if (!userFromDb.IsActive)
                {
                    // user is currently locked and we need to unlock them 
                    userFromDb.LockoutEnd = DateTime.Now;
                    await _userRepository.Update(userFromDb);
                    await _userRepository.SaveAsync();
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.SuccessMessage = "Unlock this user successfully";
                }
                else
                {
                    userFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
                    await _userRepository.Update(userFromDb);
                    await _userRepository.SaveAsync();
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.SuccessMessage = "Lock this user successfully";
                }
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Something wrong when lock/unlock user");
            }
            return BadRequest(_response);
        }

        [HttpPut("updateRole/{userId}")]
        public async Task<ActionResult<APIResponse>> RoleManagement(string userId, [FromForm] string role)
        {
            try
            {
                var oldRole = _userManager.GetRolesAsync(await _userRepository.GetAsync(u => u.Id == userId))
               .GetAwaiter().GetResult().FirstOrDefault();

                ApplicationUser applicationUser = await _userRepository.GetAsync(u => u.Id == userId);
                if (!(role == oldRole))
                {
                    if (oldRole != null)
                    {
                        _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
                    }
                    _userManager.AddToRoleAsync(applicationUser, role).GetAwaiter().GetResult();
                }
                _response.SuccessMessage = "Update this user's permission successfully";
                _response.Result = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Something wrong when update user role");
            }
            return BadRequest(_response);
        }
    }
}
