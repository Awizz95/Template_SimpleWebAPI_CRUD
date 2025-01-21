using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Template_SimpleWebAPI_CRUD.Data;
using Template_SimpleWebAPI_CRUD.Helpers.Enums;
using Template_SimpleWebAPI_CRUD.Infrastructure.Services;
using Template_SimpleWebAPI_CRUD.Models;
using Template_SimpleWebAPI_CRUD.Models.DTO.Auth;

namespace Template_SimpleWebAPI_CRUD.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _dbContext;
        private readonly TokenService _tokenService;

        public UserController(UserManager<AppUser> userManager, AppDbContext dbContext, TokenService tokenService)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userManager.CreateAsync(new AppUser
            {
                UserName = request.Username,
                Email = request.Email,
                Role = request.Role
            },
            request.Password);

            if (result.Succeeded)
            {
                request.Password = string.Empty;

                return CreatedAtAction(nameof(Register), new
                {
                    email = request.Email,
                    role = Role.User
                },
                request);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var managedUser = await _userManager.FindByEmailAsync(request.Email!);

            if (managedUser is null)
            {
                return BadRequest("Bad credentials");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password!);

            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }

            var userInDb = _dbContext.Users.FirstOrDefault(u => u.Email == request.Email);

            if (userInDb is null)
            {
                return Unauthorized();
            }

            var accessToken = _tokenService.CreateToken(userInDb);
            await _dbContext.SaveChangesAsync();

            return Ok(new AuthResponse
            {
                Username = userInDb.UserName,
                Email = userInDb.Email,
                Token = accessToken
            });
        }


    }
}
