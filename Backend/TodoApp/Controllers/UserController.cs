using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Service.DTO;
using TodoApp.Service.Interfaces;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UserController(ITokenService tokenService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserInfo model)
        {
            var user = new IdentityUser { UserName = model.UserName };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok(new { Message = "User created successfully" });
            }
            return BadRequest(result.Errors);
        }


        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(UserInfo model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (result.Succeeded)
            {
                var token = _tokenService.GenerateToken(model);
                return Ok(token);
            }
            return Unauthorized();
        }

        [HttpPost("signout")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { result = "User logged out successfully" });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(UserInfo model)
        {
            try
            {
                IdentityUser user = _userManager.Users.First(x => x.UserName == model.UserName);
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, model.Password);
                if (result.Succeeded)
                    return Ok("Password reset successful");
                return StatusCode(500, result.Errors);
            }
            catch (Exception ex)
            {
                throw new Exception("Incorrect Employee", ex);
            }
        }
    }
}
