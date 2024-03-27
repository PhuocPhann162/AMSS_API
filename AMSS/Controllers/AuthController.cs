using AMSS.Data;
using AMSS.Models.Dto.Auth;
using AMSS.Models.Dto.User;
using AMSS.Models;
using AMSS.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using AMSS.Utility;

namespace AMSS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private APIResponse _response;


        public AuthController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IMapper mapper, IUserRepository userRepository)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _userRepository = userRepository;
            _response = new();
        }
        public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationDto)
        {
            if (registrationDto.UserName == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            ApplicationUser userFromDb = await _userRepository.GetAsync(u => u.UserName.ToLower() == registrationDto.UserName.ToLower());

            if (userFromDb != null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Username already exists");
                return BadRequest(_response);
            }
            ApplicationUser newUser = new()
            {
                UserName = registrationDto.UserName,
                Email = registrationDto.UserName,
                NormalizedEmail = registrationDto.UserName.ToUpper(),
                Password = BCrypt.Net.BCrypt.HashPassword(registrationDto.Password),
                FullName = registrationDto.FullName,
                PhoneNumber = registrationDto.PhoneNumber,
                StreetAddress = registrationDto.StreetAddress,
                City = registrationDto.City,
                State = registrationDto.State,
                Country = registrationDto.Country,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            try
            {
                var result = await _userManager.CreateAsync(newUser);
                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync(registrationDto.Role).GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Farmer));
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Owner));
                    }
                    if(!String.IsNullOrEmpty(registrationDto.Role))
                    {
                        if (registrationDto.Role.ToLower() == SD.Role_Admin)
                        {
                            await _userManager.AddToRoleAsync(newUser, SD.Role_Admin);
                        }
                        else if (registrationDto.Role.ToLower() == SD.Role_Owner)
                        {
                            await _userManager.AddToRoleAsync(newUser, SD.Role_Owner);
                        }
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(newUser, SD.Role_Farmer);
                    }
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = "Registration new account successfully";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add(ex.Message);
            }
            return BadRequest(_response);
        }
    }
}
