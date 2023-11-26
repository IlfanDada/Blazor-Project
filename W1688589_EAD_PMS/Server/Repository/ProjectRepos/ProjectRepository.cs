using W1688589_EAD_PMS.Server.Data;
using W1688589_EAD_PMS.Shared.Models;
using W1688589_EAD_PMS.Shared.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Server.Repository.ProjectRepos
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext context;
        public ProjectRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }

        public async Task<Project> CreateNewProject(ProjectMV project)
        {

            //var c = await context.Project.FirstOrDefaultAsync(e => e.ProjectId == project.ProjectId);
            //c.ProjectName = project.ProjectName;
            //if (project.Workspace != null)
            //{
            //    c.WorkspaceId = project.WorkspaceId;


                Project c = new()
            {
                ProjectName = project.ProjectName,               
            };
            if (project.WorkspaceId != null)
            {
                c.WorkspaceId = project.WorkspaceId;
            }
            await context.Project.AddAsync(c);
            await context.SaveChangesAsync();
            return c;
        }



        public async Task<string> DeleteProject(int pid)
        {
        var users= await  _userManager.Users.Where(x => x.ProjectId == pid).ToListAsync();
            if (users.Count > 0)
            {
                foreach (var u in users)
                {
                    u.TicketId = null;
                    u.ProjectId = null;
                    await _userManager.UpdateAsync(u);
                }
            }

            //{
            //    ProjectName = project.ProjectName,               
            //};
            //if (project.WorkspaceId != null)
            //{
            //    c.WorkspaceId = project.WorkspaceId;
            //}
            //await context.Project.AddAsync(c);
            //await context.SaveChangesAsync();



            var tick=await  context.Ticket.Where(x => x.ProjectId == pid).ToListAsync();
            context.Ticket.RemoveRange(tick);
            await context.SaveChangesAsync();
            var proj = await context.Project.Where(x => x.ProjectId == pid).ToListAsync();
            context.Project.RemoveRange(proj);
            await context.SaveChangesAsync();
            return "delete";
        }

        public async Task<List<Project>> GetAllProjectsByWorkspaceId(int cid)
        {
            var result = await context.Project.Where(x => x.WorkspaceId == cid).ToListAsync();
            return result;
        }

        public async Task<Project> GetProjectById(int pid)
        {

            //var project = await context.Project.Where(x => x.ProjectId == pid).ToListAsync();
            //context.Project.RemoveRange(proj);
            //await context.SaveChangesAsync();

            var project = await context.Project.Where(x => x.ProjectId == pid).FirstOrDefaultAsync();
            return project;
        }

        public async Task<Project> UpdateProject(Project project)
        {
            var c = await context.Project.FirstOrDefaultAsync(e => e.ProjectId == project.ProjectId);
            c.ProjectName = project.ProjectName;
            if (project.Workspace!=null)
            {
                c.WorkspaceId = project.WorkspaceId;
            }
            await context.SaveChangesAsync();
            return c;
        }
    }
}
