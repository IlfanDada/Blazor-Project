using W1688589_EAD_PMS.Shared.Models;
using W1688589_EAD_PMS.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Server.Repository.TicketRepository
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllTicketbyProjectId(int pid);
        Task<List<ApplicationUser>> GetAllUserbyProjectId(int pid, string email);

        Task<Ticket> UpdateTicket(UpdateTicketMV tick);

        Task<TicketMV> GetTicketById(int tid);
        Task<string> CreateNewTicket(CreateTicketMV tick);

        Task<string> DeleteTicket(int tid);

        Task<bool> IsTicketAssigned(string uid);

        Task<bool> AreAllTicketsCompleted(int pid);

    }
}
