using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Sport_Match.Dtos;
using Sport_Match.Services;
using System.Security.Claims;
using Sport_Match.Services.Auth;

namespace Sport_Match.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRegistrationService _registrationService;
        private readonly IUserAuthenticationService _authenticationService;
        private readonly IAuthService _authService;

        public AccountController(
        IUserRegistrationService registrationService,
        IUserAuthenticationService authenticationService,
        Services.Auth.IAuthService authService)
        {
            _registrationService = registrationService;
            _authenticationService = authenticationService;
            _authService = authService;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

      
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = await _registrationService.RegisterAsync(model);

            if (!success)
            {
                ModelState.AddModelError("Email", "Korisnik s ovim emailom već postoji.");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var user = await _authenticationService.AuthenticateAsync(model);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Neispravan email ili lozinka.");
                return View(model);
            }


            await _authService.SignInAsync(HttpContext, user, isPersistent: true);


            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

       
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.SignOutAsync(HttpContext);

            return RedirectToAction("Index", "Home");
        }

    }
}
