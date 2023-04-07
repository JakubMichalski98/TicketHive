using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using TicketHive.Client;
using TicketHive.Client.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TicketHive.Client.Repositories;
using TicketHive.Shared.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TicketHive.Client.Pages
{
    public partial class Event
    {
        
        EventModel chosenEvent = new();
        [Parameter]
        public int Id { get; set; }

        [Required]
        [BindProperty]
        public string? NumberOfTickets { get; set; }
        protected async override Task OnInitializedAsync()
        {
            chosenEvent = await eventRepo.GetEvent(Id);
        
        }

        public void HandleSubmit()
        {
           
        }
            
    }
}