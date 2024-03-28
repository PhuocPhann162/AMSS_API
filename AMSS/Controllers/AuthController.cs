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
using BCrypt.Net;
using AMSS.Services.IService;

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
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private APIResponse _response;


        public AuthController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IMapper mapper, IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _response = new();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
                var user = await _userRepository.GetAsync(u => u.Email == loginRequestDto.Email);
                if(user == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.Unauthorized;
                    _response.ErrorMessages.Add("Username does not exist");
                    return Unauthorized(_response);
                }
                var isValid = BCrypt.Net.BCrypt.Verify(loginRequestDto.Password, user.Password);
                if(!isValid)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.Unauthorized;
                    _response.ErrorMessages.Add("Password does not exist");
                    return Unauthorized(_response);
                }

                // if user was found, generate JWT Token
                var roles = await _userManager.GetRolesAsync(user);
                var accessToken = _jwtTokenGenerator.GenerateToken(user, roles);
                var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

                UserDto userDto = new()
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.Email,
                    PhoneNumber = user.PhoneNumber,
                };

                LoginResponseDto loginResponseDto = new()
                {
                    User = userDto,
                    Token =
                    {
                        AccessToken = accessToken, 
                        RefreshToken = refreshToken,
                    }
                };
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add(ex.Message);
            }
            return BadRequest(_response);
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
