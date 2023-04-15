using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketHive.Shared.Models
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public int EventModelId { get; set; }
        public EventViewModel? EventViewModel { get; set; }
        public int Quantity { get; set; }
    }
}
