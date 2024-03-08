using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Okta.AspNetCore;

namespace OktaBlazorApp.Components.Pages
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly ILogger _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        public IActionResult SignIn([FromQuery] string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                
                return LocalRedirect(returnUrl ?? Url.Content("~/"));
            }

            return Challenge(OktaDefaults.MvcAuthenticationScheme);
        }

        public IActionResult SignOut([FromQuery] string returnUrl)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return LocalRedirect(returnUrl ?? Url.Content("~/"));
            }

            return SignOut(
                new AuthenticationProperties() { RedirectUri = Url.Content("~/") },
                new[]
                {
                    OktaDefaults.MvcAuthenticationScheme,
                    CookieAuthenticationDefaults.AuthenticationScheme,
                });
        }
    }
}
