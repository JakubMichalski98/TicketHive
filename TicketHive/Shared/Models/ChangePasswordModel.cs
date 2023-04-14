using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketHive.Shared.Models
{
    public class ChangePasswordModel
    {
        public string? Username { get; set; }
        [Required(ErrorMessage = "Old password is required")]
        public string? OldPassword { get; set; }
        [Required(ErrorMessage = "New password is required")]
        public string? NewPassword { get; set; }
    }
}
