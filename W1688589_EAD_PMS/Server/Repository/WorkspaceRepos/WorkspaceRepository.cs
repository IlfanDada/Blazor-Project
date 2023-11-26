using W1688589_EAD_PMS.Server.Data;
using W1688589_EAD_PMS.Shared.Models;
using W1688589_EAD_PMS.Shared.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Server.Repository.WorkspaceRepos //link to ApplicationDBContext + Interface
{
    public class WorkspaceRepository : IWorkspaceRepository
    {
        private ApplicationDbContext context;

        private readonly UserManager<ApplicationUser> _userManager;
        public WorkspaceRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }

        public async Task<string> CreateNewWorkspace(WorkspaceMV Workspace, string uid)
        {
        // CREATE WORKSPACE Version1?? 
        //    WorkspaceId = w.WorkspaceId,
        //    UserId = uid
        //    };

        //context.WorkspaceMember.Add();
        //    context.SaveChanges();
        //    return "Added";


            Workspace w = new()
            {
                WorkspaceName = Workspace.WorkspaceName,
                WorkspaceDescription = Workspace.WorkspaceDescription
            };
            context.Workspace.Add(w);
            context.SaveChanges();

            //context.SaveChangesAsync()

            AssignUserToWorkspace autw = new()
            {          
            WorkspaceId = w.WorkspaceId,
            UserId = uid           
            };
           
            context.WorkspaceMember.Add(autw);
            context.SaveChanges();
            return "Added";
        }

        public async Task<string> DeleteWorkspace(int cid)
        {
            var WsUser = await context.WorkspaceMember.Where(x => x.WorkspaceId == cid).ToListAsync();
            context.WorkspaceMember.RemoveRange(WsUser);
            await context.SaveChangesAsync();
            var users = await context.Project.Where(x => x.WorkspaceId == cid).ToListAsync();
            
            //try this method?
            // var users = async as ApplicationDbContext
            //await context.SaveChangesAsync();
            //THIS WORKS ?!!!!!
            //var users = await context.Project.Where(x => x.WorkspaceId == cid).ToListAsync();


            foreach (var u in users)
            {
                var user = await _userManager.Users.Where(x => x.ProjectId == u.ProjectId).FirstOrDefaultAsync();
                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
                }
                var tick = await context.Ticket.Where(x => x.ProjectId == u.ProjectId).ToListAsync();
                context.Ticket.RemoveRange(tick);
                await context.SaveChangesAsync();
            }
            context.Project.RemoveRange(users);
            await context.SaveChangesAsync();

            return "delete";
        }

        public async Task<List<Workspace>> GetAllWorkspacesByUserId(string uid)
        {
            //var Workspace = await context.Workspace.Where(x => x.WorkspaceId == cid).FirstOrDefaultAsync();
            //return ;
            var WorkspaceList = await context.WorkspaceMember.Include(x => x.Workspace).Where(m => m.UserId == uid).Select(entry => entry.Workspace).ToListAsync();
            return WorkspaceList;
        }

        public async Task<Workspace> GetWorkspaceById(int cid)
        {

            //var w = await context.Workspace.FirstOrDefaultAsync(
            //    z => z.WorkspaceId == Workspace.WorkspaceId);

            var Workspace = await context.Workspace.Where(x => x.WorkspaceId == cid).FirstOrDefaultAsync();
            return Workspace;
        }

        public async Task<Workspace> UpdateWorkspace(Workspace Workspace)
        {
            //context.Workspace.RemoveRange(tick);
            //await context.SaveChangesAsync();
            //DO NOT DELETE


            var w = await context.Workspace.FirstOrDefaultAsync(
                z => z.WorkspaceId == Workspace.WorkspaceId);


            //test code 
            //public async Task<Comp> GetWorkspaceById(int cid)
            //
            //    var Workspace = await context.Workspace.Where(x => x.WorkspaceId == cid).FirstOrDefaultAsync();
            //    return Workspace;

                w.WorkspaceName = Workspace.WorkspaceName;
            w.WorkspaceDescription = Workspace.WorkspaceDescription;
            await context.SaveChangesAsync();
            return w;
        }
    }
}
