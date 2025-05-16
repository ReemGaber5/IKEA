using IKEA.DAL.Models.Identity;
using IKEA.PL.Helpers;
using IKEA.PL.ViewModel;
using IKEA.PL.ViewModel.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IKEA.PL.Controllers
{
    public class AccountController : Controller
    {

        public UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                ModelState.AddModelError(nameof(model.UserName), "This UserName is Already In Use!");
                return View(model);

            }
            user = new ApplicationUser()
            {
                FName = model.FName,
                LName = model.LName,
                UserName = model.UserName,
                Email = model.Email,
                IsAgree = model.IsAgree,
            };
            var Result = await _userManager.CreateAsync(user, model.Password);
            if (Result.Succeeded)
                return RedirectToAction(nameof(LogIn));

            foreach (var Error in Result.Errors)
                ModelState.AddModelError(string.Empty, Error.Description);

            return View(model);

        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var User = await _userManager.FindByEmailAsync(model.Email);
            if (User != null)
            {
                var Result = await _signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, true);

                if (Result.IsNotAllowed)
                    ModelState.AddModelError(string.Empty, "Your Account Is Not Confirmed! ");

                if (Result.IsLockedOut)
                    ModelState.AddModelError(string.Empty, "Your Account Is Locked! ");

                if (Result.Succeeded)
                    return RedirectToAction(nameof(HomeController.Index), "Home");

            }
            ModelState.AddModelError(string.Empty, "Invalid Login Attempts!");
            return View(model);

        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));

        }

        //get
        public IActionResult ForgetPassword()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var Token = await _userManager.GeneratePasswordResetTokenAsync(user);//token valid for one time 
                                                                                         //this create link https\\localhost:7011\Account\ResetPassword\gmail
                    var PasswordResetLink = Url.Action("ResetPassword", "Account", new { email = user.Email, token = Token }, Request.Scheme);
                    var email = new Email()
                    {
                        Subject = "Reset Password",
                        To = user.Email,
                        Body = PasswordResetLink

                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));


                }
                ModelState.AddModelError(string.Empty, "Email Is Not Valid!");

            }
            return View(model);

        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email,string token)
        {
            //to pass them use data binding
            TempData["email"]=email;
            TempData["token"]=token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            string email = TempData["email"] as string;
            string token = TempData["token"] as string;
            var user = await _userManager.FindByEmailAsync(email);

            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
            if (result.Succeeded)
                return RedirectToAction(nameof(LogIn));

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }
    }
}
