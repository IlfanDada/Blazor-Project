using W1688589_EAD_PMS.Shared.Models;
using W1688589_EAD_PMS.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Client.Services.Projects
{
    public interface IProjectService
    {
        public Task<List<Project>> GetAllProjectsByWorkspaceId(int cid);
        Task<Project> GetProjectById(int pid);
        Task<Project> CreateNewProject(ProjectMV project);
        Task<Project> UpdateProject(Project project);
        Task DeleteProject(int pid);
    }
}
