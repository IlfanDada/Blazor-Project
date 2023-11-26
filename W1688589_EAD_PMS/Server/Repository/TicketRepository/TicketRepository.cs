using W1688589_EAD_PMS.Server.Data;
using W1688589_EAD_PMS.Shared.Models;
using W1688589_EAD_PMS.Shared.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Server.Repository.TicketRepository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext context;
        public TicketRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }

        public async Task<string> CreateNewTicket(CreateTicketMV tick)
        {
            Ticket ticket = new()
            {
                TicketName = tick.TicketName,
                Card = tick.Card,
                ProjectId = tick.ProjectId
            };
            context.Ticket.Add(ticket);
            await context.SaveChangesAsync();
            if (tick.UserList.Count > 0)
            {
                foreach (var u in tick.UserList)
                {
                    var user = await _userManager.FindByIdAsync(u);
                    if (user != null)
                    {
                        user.TicketId = ticket.TicketId;
                        await _userManager.UpdateAsync(user);
                    }

                    //public async Task<string> CreateNewTicket(CreateTicketMV tick)
                    //{
                    //    Ticket ticket = new()
                    //    {
                    //        TicketName = tick.TicketName,
                    //        Card = tick.Card,
                    //        ProjectId = tick.ProjectId


                        }
            }

            return "added";
        }

        public async Task<bool> AreAllTicketsCompleted(int pid)
        {
            int count = 0;
            var ticketlist = await context.Ticket.Where(x => x.ProjectId == pid).ToListAsync();
            if (ticketlist.Count > 0)
            {
                foreach (var tick in ticketlist)
                {
                    if (tick.Card != "Done")
                    {
                        count = 1;
                        break;
                    }
                }
                var proj = await context.Project.Where(x => x.ProjectId == pid).FirstOrDefaultAsync();
                if (count == 0)
                {

                    if (proj.Completed == false)
                    {
                        proj.Completed = true;
                        proj.EndDate = DateTime.Now;
                        await context.SaveChangesAsync();
                    }
                    return true;
                }

                //Working Method
                //    else
                //    {
                //        if (project.Completed == true)
                //        {
                //            project.Completed = false;                 
                //            await context.SaveChangesAsync();
                //        }
                //        return false;
                //    }
                //}



                //foreach (var rr in tick.UserList)
                //{
                //    var usr = await _userManager.FindByIdAsync(rr);
                //    user.TicketId = tick.TicketId;
                //    await _userManager.UpdateAsync(userrs);
                //} Doesnt work, delete later




            else
                {
                    if (proj.Completed == true)
                    {
                        proj.Completed = false;
                        proj.EndDate = null;
                        await context.SaveChangesAsync();
                    }
                    return false;
                }
            }
            return false;
        }

        public async Task<bool> IsTicketAssigned(string uid)
        {
            var user = await _userManager.FindByIdAsync(uid);
            if (user != null)
            {
                var tick = user.TicketId;
                if (tick != null)
                {
                    return true;
                }
                else { return false; }
            }
            return false;
        }

        public async Task<string> DeleteTicket(int tid)
        {
            //           
            //ReadUserMV display = new();
            //var user = await _userManager.Users.Where(x => x.Id == autw.TickeId).FirstOrDefaultAsync();
            //var roles = await _userManager.GetRolesAsync(user);
            //display.ticketId
            //


            var users = await _userManager.Users.Where(x => x.TicketId == tid).ToListAsync();
            if (users.Count > 0)
            {
                foreach (var u in users)
                {
                    u.TicketId = null;
                    await _userManager.UpdateAsync(u);
                }
            }
            var ticket = await context.Ticket.Where(x => x.TicketId == tid).FirstOrDefaultAsync();
            context.Remove(ticket);
            await context.SaveChangesAsync();
            return "delete";
        }

        public async Task<Ticket> UpdateTicket(UpdateTicketMV tick)
        {



          





            var ticket = await context.Ticket.Where(x => x.TicketId == tick.TicketId).FirstOrDefaultAsync();
            ticket.TicketName = tick.TicketName;
            await context.SaveChangesAsync();
            if (tick.UserList.Count > 0)
            {
                var usertickets = await _userManager.Users.Where(x => x.TicketId == tick.TicketId).ToListAsync();
                if (usertickets.Count > 0)
                {

                    //foreach (var l in userassignedtoticket)
                        //    TicketName = ticket.TicketName,               
                        //};
                        //if (ticket.ProjectId != null)
                        //{
                        //    l.ProjectId = ticket.ProjectId;
                        //}
                        //await context.Ticket.AddAsync(l);
                        //await context.SaveChangesAsync();


                        foreach (var ut in usertickets)
                    {
                        var user = await _userManager.FindByIdAsync(ut.Id);
                        user.TicketId = null;
                        await _userManager.UpdateAsync(user);
                    }
                }
                foreach (var rr in tick.UserList)
                {
                    var user = await _userManager.FindByIdAsync(rr);
                    user.TicketId = tick.TicketId;
                    await _userManager.UpdateAsync(user);
                }
            }
            Ticket result = new();
            result.TicketId = ticket.TicketId;
            result.ProjectId = ticket.ProjectId;
            result.Card = ticket.Card;
            result.TicketName = ticket.TicketName;
            return result;
        }

        public Task<List<Ticket>> GetAllTicketbyProjectId(int pid)
        {
            var result = context.Ticket.Where(x => x.ProjectId == pid).ToListAsync();
            return result;
        }

        public async Task<List<ApplicationUser>> GetAllUserbyProjectId(int pid, string email)
        {
            List<ApplicationUser> result = new();
            try
            {
                result = await _userManager.Users.Where(x => x.Email.Contains(email) && x.ProjectId == pid).ToListAsync();
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        public async Task<TicketMV> GetTicketById(int tid)
        {
            TicketMV ticket = new();
            var tick = await context.Ticket.Where(x => x.TicketId == tid).Include(x => x.ApplicationUsers).FirstOrDefaultAsync();
            ticket.TicketId = tick.TicketId;
            ticket.TicketName = tick.TicketName;
            ticket.ProjectId = tick.ProjectId;
            ticket.Card = tick.Card;
            ticket.UserList = new List<ApplicationUser>();
            foreach (var users in tick.ApplicationUsers)
            {
                ApplicationUser user = new()
                {
                    Id = users.Id,
                    Email = users.Email,                   
                    ProjectId = users.ProjectId,
                    TicketId = users.TicketId
                };
                ticket.UserList.Add(user);
            }
            return ticket;
        }
    }
}
