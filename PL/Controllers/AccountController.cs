using DAL.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using PL.ViewModels.Employees;
using PL.ViewModels.Identity;

namespace PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult SignUp()
        {
            return View();
        }
        #region old signup
        //    [HttpPost]
        //    public async Task<IActionResult> SignUp(SignUpVM model) { 


        //        if (!ModelState.IsValid)
        //            return BadRequest();

        //        var user = await _userManager.FindByNameAsync(model.UserName);
        //        if (user == null)
        //        {
        //            user = new ApplicationUser
        //            {
        //                FName = model.FirstName,
        //                LName = model.LastName,
        //                Email = model.Email,
        //                UserName = model.UserName,
        //                IsAgree = model.IsAgree



        //            };
        //            var res = await _userManager.CreateAsync(user,model.Password);
        //            if (res.Succeeded)
        //                return RedirectToAction(nameof(SignIn));
        //            foreach (var er in res.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, er.Description);

        //            }


        //        }
        //        else
        //            ModelState.AddModelError(nameof(SignUpVM.UserName), "this user name is already token");

        //    return View(model);

        //    
        //}
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken] // Always a good idea for security
        public async Task<IActionResult> SignUp(SignUpVM model)
        {
            if (!ModelState.IsValid)
            {
                
                return View(model);
            }

          
            var userByEmail = await _userManager.FindByEmailAsync(model.Email);
            var userByName = await _userManager.FindByNameAsync(model.UserName);

            if (userByEmail != null || userByName != null)
            {
                ModelState.AddModelError(string.Empty, "A user with this email or username already exists.");
                return View(model);
            }

            var user = new ApplicationUser
            {
                FName = model.FirstName,
                LName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
                IsAgree = model.IsAgree
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        public IActionResult SignIn() { 
        
        
        return View();
        }
        #region old login
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> SignIn(LoginViewModel model) {

        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    var user = await _userManager.FindByEmailAsync(model.Email);
        //    if (user is { })
        //    {

        //        var flag = await _userManager.CheckPasswordAsync(user, model.Password);
        //        if (flag)
        //        {
        //            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
        //            if (result.IsNotAllowed)
        //                ModelState.AddModelError(string.Empty, "your account is not confirmed yet");
        //            if (result.IsLockedOut)
        //                ModelState.AddModelError(string.Empty, "your accout is lockeddd!!");

        //            if (result.Succeeded)
        //                return RedirectToAction(nameof(HomeController.Index), "Home");
        //        }
        //    }

        //return View(model);
        //}
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Return the view so client-side errors are displayed
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                // This single line attempts to sign the user in.
                // It automatically checks the password.
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }

                // Handle specific login failures like lockout or unconfirmed email
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "This account has been locked out.");
                }
                else if (result.IsNotAllowed)
                {
                    ModelState.AddModelError(string.Empty, "Sign-in is not allowed. Please confirm your email.");
                }
                else
                {
                    // This handles the case of a wrong password
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            else
            {
                // This handles the case where the user email was not found.
                // We use the same generic error for security.
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            // If we reach this point, login failed for some reason. Return to the view to show the error.
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
