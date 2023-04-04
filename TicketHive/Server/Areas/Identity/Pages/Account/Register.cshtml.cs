using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TicketHive.Server.Data;
using TicketHive.Server.Models;

namespace TicketHive.Server.Areas.Identity.Pages.Account
{
    [BindProperties]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;




        [Required(ErrorMessage = "Username is required")]
        [MinLength(5, ErrorMessage = "Username needs to be 5 characters")]

        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(5,ErrorMessage = "Password needs to be 5 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm your password")]
        public string ConfirmPassword { get; set; }

        public RegisterModel(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }



        public void OnGet()
        {
        }

        public async Task <IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                ApplicationUser Newuser = new()
                {
                    UserName = Username,
                };

                if(Password == ConfirmPassword)
                {
                    var registerResult = await signInManager.UserManager.CreateAsync(Newuser, Password);

                    if (registerResult.Succeeded)
                    {  
                        var signInResult = await signInManager.PasswordSignInAsync(Username!, Password!, false, false);


                        return Redirect("~/");
                    }

                }




            }

            return Page();        
        }
    }
}
