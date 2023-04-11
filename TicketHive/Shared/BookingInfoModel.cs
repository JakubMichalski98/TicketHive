using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketHive.Shared.Models;

namespace TicketHive.Shared
{
    public class BookingInfoModel
    {
        public UserModel? User { get; set; }
        public BookingModel? Booking { get; set; }
    }
}
