using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOS.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    public class AuthenticationController : APIBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login (LoginDto loginDto , CancellationToken ct)
        {
            return ToActionResult(await _authenticationService.LoginAsync(loginDto , ct));
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto , CancellationToken ct)
        {
            return ToActionResult(await _authenticationService.RegisterAsync(registerDto, ct));
        }
    }
}
