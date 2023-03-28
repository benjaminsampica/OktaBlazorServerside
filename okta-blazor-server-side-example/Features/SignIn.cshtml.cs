using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Okta.AspNetCore;

namespace okta_blazor_server_side_example.Features;

public class SignIn : PageModel
{
    public IActionResult OnGet([FromQuery] string returnUrl)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return LocalRedirect(returnUrl ?? Url.Content("~/"));
        }

        return Challenge(OktaDefaults.MvcAuthenticationScheme);
    }
}
