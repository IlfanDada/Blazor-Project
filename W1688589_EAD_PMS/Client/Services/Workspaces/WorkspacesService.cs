using W1688589_EAD_PMS.Shared.Models;
using W1688589_EAD_PMS.Shared.ViewModels;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Client.Services.Workspaces
{
    public class WorkspaceService : IWorkspaceService
    {

        private readonly HttpClient httpClient;
        public WorkspaceService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> CreateNewWorkspace(WorkspaceMV Workspace)
        {
           
                var result = await httpClient.PostAsJsonAsync("api/Workspace/AddNewWorkspace", Workspace);
              
                var res = await result.Content.ReadAsStringAsync();
                return res;
            

        }

        public async Task DeleteWorkspace(int cid)
        {
             
                var result = await httpClient.GetAsync($"api/Workspace/DeleteWorkspace/{cid}");
        }

        public async Task<List<Workspace>> GetAllWorkspacesByUserId()
        {
        
                var result = await httpClient.GetFromJsonAsync<List<Workspace>>("api/Workspace/GetAllCompaniesByUserid");
                return result;
        }

        public async Task<Workspace> GetWorkspaceById(int cid)
        {
            try
            {
               
                var result = await httpClient.GetFromJsonAsync<Workspace>($"api/Workspace/GetWorkspaceById/{cid}");
                return result;
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
                return null;
            }
        }

        public async Task<Workspace> UpdateWorkspace(Workspace Workspace)
        {
          
            var result = await httpClient.PostAsJsonAsync($"api/Workspace/UpdateWorkspace", Workspace);
         
            var res = result.Content.ReadFromJsonAsync<Workspace>();
            return await res;
        }
    }
}
