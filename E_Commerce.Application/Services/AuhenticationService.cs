using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOS.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    public class AuhenticationService : IAuthenticationService
    {
        private readonly IIdentityService _identityService;

        public AuhenticationService(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
        {
            var userResult = await _identityService.FindUserByEmailAsync(loginDto.Email);
            if (!userResult.IsSucess)
                return Result<UserDto>.Fail(userResult.Errors);

            var passwordResult = await _identityService.CheckPasswordAsync(loginDto.Email, loginDto.Password);
            if (!passwordResult.IsSucess)
                return Result<UserDto>.Fail(userResult.Errors);

            if (!passwordResult.data)
                return Result<UserDto>.Fail(Error.Unauthorized("Invalid Email Or Password"));

            return new UserDto()
            {
                Email = loginDto.Email,
                DisplayName = userResult.data.DisplayName,
                Tokens = "Tokens"
            };
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            var result = await _identityService.CreateUserAsync(registerDto, ct);
            if(!result.IsSucess)
            {
                return Result<UserDto>.Fail(result.Errors);
            }
            var user = result.data;
            return Result<UserDto>.Ok(new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Tokens = "Tokens"
            });
        }
    }
}
