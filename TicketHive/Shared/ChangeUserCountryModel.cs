using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketHive.Shared
{
    public class ChangeUserCountryModel
    {
        public string? Username { get; set; }
        
        public string? UserCountry { get; set; }
    }
}
