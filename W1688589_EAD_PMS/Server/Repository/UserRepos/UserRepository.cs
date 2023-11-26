using W1688589_EAD_PMS.Server.Data;
using W1688589_EAD_PMS.Shared.Models;
using W1688589_EAD_PMS.Shared.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Server.Repository.UserRepos
{
    public class UserRepository : IUserRepository
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;
        public UserRepository(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

      

        public async Task<string> ChangeUserRole(string uid, string role)
        {
            //  var user = await _userManager.AddToRoleAsync(uid);
            //if (role == "Admin") // DO  I NEED FOR SUPER ADMIN???
            //{
            //    var result = await _userManager.AddToRoleAsync(user, role);
            //    if (result.Succeeded)
            //    {
            //        await _userManager.RemoveFromRoleAsync(user, "Normal"); // ALLOW ADMIN TO SET SUB TO NORMAL?
            //    }

            var user = await _userManager.FindByIdAsync(uid);
            if (role == "SubAdmin")
            {
                var result = await _userManager.AddToRoleAsync(user, role);
                if (result.Succeeded)
                {
                    await _userManager.RemoveFromRoleAsync(user, "Normal");
                }
            }

            //This method has errors, try the identity Method Instead
            //var user = await _userManager.AddToRoleAsync(uid);
            //if (role == "SubAdmin")
            //{
            //    var result = await _userManager.AddToRoleAsync(user, role);
            //    if (result.Succeeded)
            //    {
            //        await _userManager.RemoveFromRoleAsync(user, "Normal");
            //    }
            //}



            if (role == "Normal")
            {
                var result = await _userManager.AddToRoleAsync(user, role);
                if (result.Succeeded)
                {
                    await _userManager.RemoveFromRoleAsync(user, "SubAdmin");
                }
            }
            return "Changed";
        }

        public async Task<string> CreateNewUser(UserMV users)
        {
            string message = "";
            var user = new ApplicationUser { UserName = users.Email, Email = users.Email, ProjectId = users.ProjectId };
       
            var result = await _userManager.CreateAsync(user, users.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Normal");
                message = "Added";
            }
            foreach (var error in result.Errors)
            {
                if (error.Code == "DuplicateUserName")
                {
                    message = "Duplicate";
                }
            }

            // TEST DO NOT DELETE
            //{    
            //    Assignabc to a  = new()
            //    {
            //        Id = (int)users.WorkspaceId,
            //        UsrId = user.Id
            //    };
            //}

            if (message == "Added")
            {
                AssignUserToWorkspace autw = new()
                {
                    WorkspaceId = (int)users.WorkspaceId,
                    UserId = user.Id
                };
                _context.WorkspaceMember.Add(autw);
                _context.SaveChanges();
            }


            return message;
        }

        public async Task<string> DeleteUser(string uid)
        {
            //DELETE ALL USER DETAILS AND ALL CORRESPONDANCE LIKE THIS?
            //ReadUserMV display = new();
            //var user = await _userManager.Users.Where(x => x.Id == autw.UserId).FirstOrDefaultAsync();
            //var roles = await _userManager.GetRolesAsync(user);
            //display.UserId = user.Id;
            //display.Email = user.Email;
            //display.TicketId = user.TicketId;
            //FAIL ABOVE 

            //await _context.SaveChangesAsync();
            //var user = await _userManager.FindidbyAllWorkspaces
            //var result = await _userManager.DeleteAsync(user);
            //?? 


            var delete = await _context.WorkspaceMember.Where(x => x.UserId == uid).ToListAsync();
            _context.WorkspaceMember.RemoveRange(delete);
            await _context.SaveChangesAsync();
            var user = await _userManager.FindByIdAsync(uid);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return "deleted";
            }
            return "error";
        }

        public Task<List<ApplicationUser>> GetAllUserByPid(int pid)
        {
            var result = _userManager.Users.Where(x => x.ProjectId == pid).ToListAsync();
            return result;
        }

        public async Task<List<ReadUserMV>> GetAllUserByWorkspaceID(int cid)
        {
            List<ReadUserMV> displayList = new();
            var list = await _context.WorkspaceMember.Where(x => x.WorkspaceId == cid).ToListAsync();
            if (list.Count > 0)
            {
                foreach (var autw in list)
                {
                    ReadUserMV display = new();
                    var user = await _userManager.Users.Where(x => x.Id == autw.UserId).FirstOrDefaultAsync();
                    var roles = await _userManager.GetRolesAsync(user);
                    display.UserId = user.Id;
                    display.Email = user.Email;
                    display.TicketId = user.TicketId;
                    if (user.TicketId != null)
                    {
                        var ticket = await _context.Ticket.Where(x => x.TicketId == user.TicketId).FirstOrDefaultAsync();
                        display.TicketName = ticket.TicketName;
                    }
                    else
                    {
                        display.TicketName = null;
                    }
                    if (user.ProjectId != null)
                    {
                        var proj = await _context.Project.Where(x => x.ProjectId == user.ProjectId).FirstOrDefaultAsync();
                        display.ProjectName = proj.ProjectName;
                    }

                    else
                    {
                        display.ProjectName = null;
                    }

                    //if (user.userId != null)
                    //{
                    //    var user = await _context.User.Where(x => x.UserId == user.UserId).FirstOrDefaultAsync();
                    //    display.UserName = user.UserName;
                    //}

                    //else
                    //{
                    //    display.UserName = null;
                    //}


                    if (roles[0] == "Admin")
                    {
                        display.Role = "Admin";
                    }
                    else if (roles[0] == "SubAdmin")
                    {
                        display.Role = "SubAdmin";
                    }
                    else
                    {
                        display.Role = "Normal";
                    }
                    displayList.Add(display);
                }
            }
            return displayList;
        }

        public async Task<ApplicationUser> GetUserById(string uid)
        {
            var result = await _userManager.Users.Where(x => x.Id == uid).FirstOrDefaultAsync();
            ApplicationUser user = new()
            {
                Id = result.Id,
                Email = result.Email
            };
            user.ProjectId = user.ProjectId;
            return user;
        }

        public async Task<List<ReadUserMV>> SearchUsers(int cid, string email)
        {
            List<ReadUserMV> displayList = new();
            try
            {
                var list = await _context.WorkspaceMember.Where(x => x.WorkspaceId == cid).ToListAsync();
                if (list.Count > 0)
                {
                    foreach (var cm in list)
                    {
                        ReadUserMV display = new();
                        var user = await _userManager.Users.Where(x => x.Id == cm.UserId && x.Email.Contains(email)).FirstOrDefaultAsync();
                        if (user != null)
                        {
                            var roles = await _userManager.GetRolesAsync(user);
                            display.UserId = user.Id;
                            display.Email = user.Email;
                            display.TicketId = user.TicketId;
                            if (user.TicketId != null)
                            {
                                var ticket = await _context.Ticket.Where(x => x.TicketId == user.TicketId).FirstOrDefaultAsync();
                                display.TicketName = ticket.TicketName;
                            }
                            else
                            {
                                display.TicketName = null;
                            }
                            if (user.ProjectId != null)
                            {
                                var p = await _context.Project.Where(x => x.ProjectId == user.ProjectId).FirstOrDefaultAsync();
                                display.ProjectName = p.ProjectName;
                            }

                            //if (user.userId != null)
                            //{
                            //    var user = await _context.User.Where(x => x.UserId == user.UserId).FirstOrDefaultAsync();
                            //    display.UserName = user.UserName;
                            //}

                            //else
                            //{
                            //    display.UserName = null;
                            //}
                            else
                            {
                                display.ProjectName = null;
                            }
                            
                            if (roles[0] == "Admin")
                            {
                                display.Role = "Admin";
                            }
                            else if (roles[0] == "SubAdmin")
                            {
                                display.Role = "SubAdmin";
                            }
                            else
                            {
                                display.Role = "Normal";
                            }
                            displayList.Add(display);
                        }

                    }
                }
                return displayList;
            }
            catch (Exception)
            {
                return displayList;
            }

        }

        public async Task<string> UpdateUser(UpdateUserMV users)
        {

            var user = await _userManager.FindByIdAsync(users.UserId);
            if (user.Email != users.Email)
            {
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, users.Email);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var result1 = await _userManager.ChangeEmailAsync(user, users.Email, code);
                if (!result1.Succeeded)
                {
                    return "AlreadyExist";
                }
            }
            user.ProjectId = users.ProjectId;
            await _userManager.UpdateAsync(user);
            return "updated";
        }
    }
}
