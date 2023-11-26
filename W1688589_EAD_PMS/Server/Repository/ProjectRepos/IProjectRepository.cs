using W1688589_EAD_PMS.Shared.Models;
using W1688589_EAD_PMS.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Server.Repository.ProjectRepos
{
    public interface IProjectRepository
    {
        Task<Project> GetProjectById(int pid);
        Task<string> DeleteProject(int pid);
        Task<Project> UpdateProject(Project project);
        Task<List<Project>> GetAllProjectsByWorkspaceId(int cid);
        Task<Project> CreateNewProject(ProjectMV project);

    }
}
