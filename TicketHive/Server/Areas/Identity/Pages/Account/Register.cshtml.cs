using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using TicketHive.Server.Data;
using TicketHive.Server.Enums;
using TicketHive.Server.Models;
using TicketHive.Shared.Models;

namespace TicketHive.Server.Areas.Identity.Pages.Account
{
    [BindProperties]
    public class RegisterModel : PageModel
    {

        EventDbContext _Context;

        private readonly SignInManager<ApplicationUser> signInManager;

        [Required(ErrorMessage = "Username is required")]
        [MinLength(5, ErrorMessage = "Username needs to be 5 characters")]

        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(5,ErrorMessage = "Password needs to be 5 characters")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        [Required(ErrorMessage = "Confirm your password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please select a country to proceed")] 
        public string SelectedCountry { get; set; }
       



        public RegisterModel(SignInManager<ApplicationUser> signInManager, EventDbContext context)
        {
            this.signInManager = signInManager;
            _Context = context;
        }



        public void OnGet()
        {
        }

        public async Task <IActionResult> OnPost()
        {
            if (_Context.Users.Any(u => u.Username == Username))
            {
                ModelState.AddModelError("Username", "Username already exists");
            }

            if (ModelState.IsValid)
            {
                ApplicationUser Newuser = new()
                {
                    UserName = Username,
                    UserCountry = SelectedCountry,
                };
                UserModel NewEventUser = new()
                {
                    Username = Username, 
                    UserCountry = SelectedCountry,
                    
                };
                _Context.Users.Add(NewEventUser);
                _Context.SaveChanges();

                if(Password == ConfirmPassword)
                {
                    var registerResult = await signInManager.UserManager.CreateAsync(Newuser,Password);

                    if (registerResult.Succeeded)
                    {  
                        var signInResult = await signInManager.PasswordSignInAsync(Username!, Password!, false, false);


                        return Redirect("/home");
                    }

                }
               
            }

            return Page();        
        }
    }
}
