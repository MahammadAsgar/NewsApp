using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsMedia.Infrastructure.DTOS.Users;
using NewsMedia.Infrastructure.Services.Mail;
using NewsMedia.Infrastructure.Services.Users.Abstractions;

namespace NewsMediaApp.Controllers
{
    public class AuthController : Controller
    {
        readonly IAuthService _authService;
        readonly IMailService _mailService;
        readonly IHttpContextAccessor _contextAccessor;
        public AuthController(IAuthService authService, IMailService mailService, IHttpContextAccessor contextAccessor)
        {
            _authService = authService;
            _mailService = mailService;
            _contextAccessor = contextAccessor;
        }
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [Route("Auth/SignUp")]
        public async Task<IActionResult> SignUp(SignUpDTO signUpDTO)
        {
            if (ModelState.IsValid)
            {
                var objects = await _authService.SignUp(signUpDTO);
                var confirmationLink = Url.Action("ConfirmEmail", "Auth", new { userId = objects.Item1.Id, token = objects.Item2 }, Request.Scheme);
                await _mailService.SendMessage(signUpDTO.Email, $"This is your confirmation link: {confirmationLink}");
            }
            return RedirectToAction("SignIn", "Auth");
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(int userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }
            var result = await _authService.ConfirmEmail(userId, token);
            if (result.Succeeded)
            {
                return View();
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [Route("Auth/SignIn")]
        public async Task<IActionResult> SignIn(SignInDto signInDto)
        {
            string role = string.Empty;
            if (ModelState.IsValid)
            {
                var token = await _authService.SignIn(signInDto);
                _contextAccessor.HttpContext.Session.SetString("token", token.Item1.AccessToken);
                role = token.Item2;
            }
            if (role == "Admin")
            {
                return RedirectToAction("Home", "Admin");
            }
            else if (role == "Moderator")
            {
                return RedirectToAction("Home", "Moderator");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await _authService.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
