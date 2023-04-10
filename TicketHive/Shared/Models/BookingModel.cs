using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TicketHive.Shared.Models
{
    public class BookingModel
    {
        public int Id { get; set; }
        public int EventModelId { get; set; }
        public EventModel? EventModel { get; set; }
        public int Quantity { get; set; }
    }
}
