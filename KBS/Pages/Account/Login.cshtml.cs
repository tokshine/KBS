using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using KO.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace KBS.Pages.Account
{
    public class LoginModel : PageModel
    {
       

        private readonly ILogger<LoginModel> _logger;
        private readonly SignInManager<KoUser> _signInManager;
        public LoginModel(ILogger<LoginModel> logger,SignInManager<KoUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        [BindProperty] // 2 way binding
        public LoginViewModel loginViewModel { get; set; }

        public IActionResult OnGet()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                //return RedirectToAction("ClientLanguages","Language"); this would not work since there is no actual controller
                return RedirectToPage("../Knowledge/ClientLanguages");
            }
            return Page();
        }

        //routing and parameters
        //http://learningprogramming.net/net/asp-net-core-razor-pages/parameters-to-routes-in-asp-net-core-razor-pages/
       
        public async Task<IActionResult> OnGetLogout()
        {
            await _signInManager.SignOutAsync();

            return   RedirectToPage("../Index");
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
               var result = await _signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password,loginViewModel.RememberMe,false);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }
                    else {
                        //return RedirectToAction("ClientLanguages", "Language");
                        return RedirectToPage("../Knowledge/ClientLanguages");
                    }

                }
            }
            ModelState.AddModelError("","Failed to login");
            return Page();
        }
    }
}