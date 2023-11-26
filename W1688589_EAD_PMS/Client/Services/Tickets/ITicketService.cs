using W1688589_EAD_PMS.Shared.Models;
using W1688589_EAD_PMS.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Client.Services.Tickets
{
    public interface ITicketService
    {
        Task<List<Ticket>> GetAllTicketbyProjectId(int pid);
        Task<List<ApplicationUser>> GetAllUsersbyProjectId(int pid,string email);
        Task<bool> IsTicketAssigned(string uid);
        Task<string> AddTickets(CreateTicketMV tick);
        Task<TicketMV> GetTicketById(int tid);
        Task DeleteTicket(int tid);
        Task<bool> AreAllTicketsCompleted(int pid);
        Task<Ticket> UpdateTicket(UpdateTicketMV tick);
    }
}
