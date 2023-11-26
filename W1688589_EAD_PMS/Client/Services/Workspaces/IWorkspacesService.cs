using W1688589_EAD_PMS.Shared.Models;
using W1688589_EAD_PMS.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Client.Services.Workspaces
{
    public interface IWorkspaceService
    {
        public Task<List<Workspace>> GetAllWorkspacesByUserId();
        public Task<Workspace> GetWorkspaceById(int cid);
        public Task<string> CreateNewWorkspace(WorkspaceMV Workspace);
        public Task<Workspace> UpdateWorkspace(Workspace Workspace);
        Task DeleteWorkspace(int cid);
    }
}
