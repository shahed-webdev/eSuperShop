using eSuperShop.BusinessLogic;
using eSuperShop.Data;
using eSuperShop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eSuperShop.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ICustomerCore _customer;

        public AccountController(IUnitOfWork db, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ICustomerCore customer)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _customer = customer;
        }

        //GET: Admin/seller Login
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        //POST: Admin/seller Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                var type = _db.Registration.UserTypeByUserName(model.UserName);

                return type switch
                {
                    UserType.Admin => LocalRedirect(returnUrl ??= Url.Content("~/Dashboard/Index")),
                    UserType.Seller => LocalRedirect(returnUrl ??= Url.Content("~/Dashboard/Seller")),
                    UserType.SubAdmin => LocalRedirect(returnUrl ??= Url.Content("~/Dashboard/Index")),
                    _ => LocalRedirect(returnUrl ??= Url.Content("~/Account/Login"))
                };
            }

            if (result.RequiresTwoFactor) return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, model.RememberMe });

            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }


        //GET: Customer Registration
        [AllowAnonymous]
        public IActionResult CustomerRegistration(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        //GET: Customer Login
        [AllowAnonymous]
        public IActionResult CustomerLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        //POST: Customer Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CustomerLogin(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (result.Succeeded)
               return LocalRedirect(returnUrl ??= Url.Content("~/Customer/Dashboard"));

            if (result.RequiresTwoFactor)
                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, model.RememberMe });

            if (result.IsLockedOut)
                return RedirectToPage("./Lockout");


            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }


        // GET: ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        // POST: ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }

            await _signInManager.RefreshSignInAsync(user);

            return RedirectToAction("ChangePassword", "Account", new { Message = "Your password has been changed." });
        }

        //POST: logout
        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();

            if (returnUrl != null) return LocalRedirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        //POST: ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }


        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl ??= Url.Content("~/Customer/Dashboard");

            if (remoteError != null)
            {
                return RedirectToAction("CustomerLogin", new { ReturnUrl = returnUrl });
            }

            // Get the login information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("CustomerLogin", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction("Lockout");
            }

            // Get the email claim value
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            if (email != null)
            {
                // Create a new user without password if we do not have a user already
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    var customerModel = new CustomerAddModel
                    {
                        UserName = email,
                        Name = info.ProviderDisplayName,
                        Email = email
                    };
                    var response = _customer.Add(customerModel);
                    if (!response.IsSuccess)
                        return RedirectToAction("CustomerLogin", new { ReturnUrl = returnUrl });

                    user = new IdentityUser
                    {
                        UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                    };

                    await _userManager.CreateAsync(user);
                    await _userManager.AddToRoleAsync(user, UserType.Customer.ToString()).ConfigureAwait(false);

                }

                // Add a login (i.e insert a row for the user in AspNetUserLogins table)
                await _userManager.AddLoginAsync(user, info);
                await _signInManager.SignInAsync(user, isPersistent: false);

                return LocalRedirect(returnUrl);
            }

            // If we cannot find the user email we cannot continue
            ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
            ViewBag.ErrorMessage = "Please contact support on info@esupershop.com";

            return View("Error");
        }

        //access denied user
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}