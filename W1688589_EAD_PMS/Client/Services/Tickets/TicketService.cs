using W1688589_EAD_PMS.Shared.Models;
using W1688589_EAD_PMS.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Client.Services.Tickets
{
    public class TicketService : ITicketService
    {
        private readonly HttpClient httpClient;
        public TicketService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> AddTickets(CreateTicketMV tick)
        {
           
            var result = await httpClient.PostAsJsonAsync("api/ticket/AddTickets", tick);
         
            var res = await result.Content.ReadAsStringAsync();
            return res;
        }

        public async Task<bool> AreAllTicketsCompleted(int pid)
        {
     
            var result = await httpClient.GetFromJsonAsync<bool>($"api/ticket/AreAllTicketsCompleted/{pid}");
            return result;
        }

        public async Task<bool> IsTicketAssigned(string uid)
        {
          
            var result = await httpClient.GetFromJsonAsync<bool>($"api/ticket/IsTicketAssigned/{uid}");
            return result;
        }

        public async Task DeleteTicket(int tid)
        {
          
            var result = await httpClient.GetAsync($"api/ticket/DeleteTicket/{tid}");
        }

        public async Task<Ticket> UpdateTicket(UpdateTicketMV tick)
        {
    
            var result = await httpClient.PostAsJsonAsync("api/ticket/UpdateTicket", tick);

            var res = await result.Content.ReadFromJsonAsync<Ticket>();
            return res;

        }

        public async Task<List<Ticket>> GetAllTicketbyProjectId(int pid)
        {
   
            var result = await httpClient.GetFromJsonAsync<List<Ticket>>($"api/ticket/GetAllTicketbyProjectId/{pid}");
            return result;
        }
        public async Task<List<ApplicationUser>> GetAllUsersbyProjectId(int pid, string email)
        {
            List<ApplicationUser> result = new();
            if (!string.IsNullOrEmpty(email))
            {
        
                result = await httpClient.GetFromJsonAsync<List<ApplicationUser>>($"api/ticket/GetAllUsersbyProjectId/{pid}/{email}");
            }
            return result;
        }

        public async Task<TicketMV> GetTicketById(int tid)
        {
            try
            {
             
                var result = await httpClient.GetFromJsonAsync<TicketMV>($"api/ticket/GetTicketById/{tid}");
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
