using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SaleInvoicesApp.Models;

namespace SaleInvoicesApp.Controllers
{
    [Authorize(Roles = "Admin, Cashier")]
    public class AccountController : Controller
    {
        private readonly UserManager<SalesStaff> _userManager;
        private readonly SignInManager<SalesStaff> _signInManager;

        public AccountController(UserManager<SalesStaff> userManager,
                                      SignInManager<SalesStaff> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Register Cashier
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(SalesStaff model)
        {
            if (ModelState.IsValid)
            {
                var user = new SalesStaff
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                };

                var result = await _userManager.CreateAsync(user, model.Password);//Create (add) record for user in database user table.

                if (result.Succeeded)
                {
                    //add user role as Cashier in db.
                    await _userManager.AddToRoleAsync(user, Enums.Roles.Cashier.ToString());
                    //After adding user sign in this user automatically.
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Welcome");
                    //return RedirectToAction("Index", "Inventory");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(model);
        }

        /// <summary>
        /// Show login user view (.cshtml).
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Login user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel user)
        {
            if (ModelState.IsValid)
            {
                //Check if user added correct email and password.
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

                if (result.Succeeded)//if credentials added are correct then redirect to Index method of Home controller. 
                {
                    return RedirectToAction("Index", "Welcome");
                    //return RedirectToAction("Index", "Inventory");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(user);
        }

        /// <summary>
        /// Signout (Logout) the user.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}
