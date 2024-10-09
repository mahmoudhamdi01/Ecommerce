using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IDentityEntities;
using Store.Service.Services.TokenServices;
using Store.Service.Services.userService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.userService
{
    public class UserServices : IUserService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenServices _tokenServices;

        public UserServices(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenServices tokenServices)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenServices = tokenServices;
        }
        public async Task<UserDto> Login(LoginDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);
            if (user is null)
                return null;
            var result = await _signInManager.CheckPasswordSignInAsync(user, input.Password, false);
            if (!result.Succeeded)
                throw new Exception("Login Failed");

            return new UserDto
            {

                Email = input.Email,
                Id = Guid.Parse(user.Id),
                DisplayName = user.DisplayName,
                Token = _tokenServices.GenerateToken(user)
            };

        }

        public async Task<UserDto> Register(RegisterDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);

            if (user is null)
                return null;

            var appuser = new AppUser
            {
                DisplayName = input.DisplayName,
                Email = input.Email,
                UserName = input.DisplayName,
            };

            var result = await _userManager.CreateAsync(appuser, input.Password);
            if (!result.Succeeded)
                throw new Exception(result.Errors.Select(x => x.Description).FirstOrDefault());

            return new UserDto
            {

                Email = appuser.Email,
                Id = Guid.Parse(appuser.Id),
                DisplayName = appuser.DisplayName,
                Token = _tokenServices.GenerateToken(appuser)
            };
        }

    }
}
