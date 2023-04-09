using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketHive.Shared.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        public string EventName { get; set; } = null!;
        public string EventType { get; set; } = null!;
        public string EventPlace { get; set; } = null!;
        public string EventDetails { get; set; } = null!;
        public DateTime Date { get; set; }
        public decimal PricePerTicket { get; set; }
        public int TotalTickets { get; set; }
        public int SoldTickets { get; set; }
        public string? Image { get; set; }
    }
}
