using W1688589_EAD_PMS.Shared.Models;
using W1688589_EAD_PMS.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Server.Repository.WorkspaceRepos
{
    public interface IWorkspaceRepository
    {

        Task<Workspace> UpdateWorkspace(Workspace Workspace);

        Task<string> CreateNewWorkspace(WorkspaceMV Workspace, string uid);
        Task<Workspace> GetWorkspaceById(int cid);

        Task<List<Workspace>> GetAllWorkspacesByUserId(string uid);

        Task<string> DeleteWorkspace(int cid);
    }
}
