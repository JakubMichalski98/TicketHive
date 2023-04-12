using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TicketHive.Shared.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string? UserCountry { get; set; }
        public List<BookingModel> Bookings { get; set; } = new();
       
    }
}
