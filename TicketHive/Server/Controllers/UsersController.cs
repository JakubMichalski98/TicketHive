
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketHive.Server.Data;
using TicketHive.Server.Models;
using TicketHive.Shared;
using TicketHive.Shared.Models;
using TicketHive.Client.Repositories;
using System.Diagnostics.Metrics;

namespace TicketHive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly EventDbContext context;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UsersController(EventDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            this.context = context;
            this.signInManager=signInManager;
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<UserModel>> GetUser(string username)
        {
            UserModel? userModel = await context.Users.Include(u => u.Bookings).ThenInclude(b => b.EventModel).FirstOrDefaultAsync(u => u.Username == username);

            if (userModel != null)
            {
                return Ok(userModel);
            }
            return NotFound("User with provided username not found");
        }

        [HttpPost]
        public async Task<ActionResult<List<UserModel>>> AddBookingToUser(BookingInfoModel bookingInfo)
        {
            var foundUser = context.Users.FirstOrDefault(u => u.Username == bookingInfo.User.Username);

            if (foundUser != null)
            {
                foundUser.Bookings.Add(bookingInfo.Booking);
                await context.SaveChangesAsync();
                return (Ok("Booking added to user"));
            }
            return BadRequest("Something went wrong when adding booking to user");
        }

        [HttpPut]
        public async Task<IActionResult> ChangeUserPassword(ChangePasswordModel changePasswordModel)
        {
            var applicationUser = await signInManager.UserManager.FindByNameAsync(changePasswordModel.Username!);

            if(applicationUser != null)
            {
                var changePasswordResult = await signInManager.UserManager.ChangePasswordAsync(applicationUser, changePasswordModel.OldPassword!, changePasswordModel.NewPassword!);

                if(changePasswordResult.Succeeded)
                {
                    return Ok();
                }
            }

            return BadRequest();
        }

        [HttpPut("country")]
        public async Task<IActionResult> ChangeUserCountry(ChangeUserCountryModel changeUserCountryModel)
        {
            UserModel user = await context.Users.FirstOrDefaultAsync(u => u.Username == changeUserCountryModel.Username);

            if (user != null)
            {
                user.UserCountry = changeUserCountryModel.UserCountry;
                await context.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }
        [HttpPut("currency")]
        public async Task SetUserCurrency([FromBody] string username)
        {
            UserModel? user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            string currency = "";

            if (user.UserCountry == "England" || user.UserCountry == "Ireland" || user.UserCountry == "Northern_Ireland" || user.UserCountry == "Scotland")
            {
                currency = "£";
            }
            else if (user.UserCountry == "Sweden")
            {
                currency =  "SEK";
            }
            else
            {
                currency = "€";
            }
            if (user != null)
            {
                user.Currency = currency;
                await context.SaveChangesAsync();
            }
        }


    }
}
