using AspNet.Security.OAuth.Okta;
using Microsoft.AspNetCore.Mvc;

namespace okta_blazor_server_side_example.Features;
public class SignInController : Controller
{
    [Route("account/signin")]
    public IActionResult SignIn([FromQuery] string returnUrl)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return LocalRedirect(returnUrl ?? Url.Content("~/"));
        }

        return Challenge(OktaAuthenticationDefaults.AuthenticationScheme);
    }
}