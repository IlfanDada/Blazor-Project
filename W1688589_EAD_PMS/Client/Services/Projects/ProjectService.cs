using W1688589_EAD_PMS.Shared.Models;
using W1688589_EAD_PMS.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Client.Services.Projects
{
    public class ProjectService : IProjectService
    {
        private readonly HttpClient httpClient;
        public ProjectService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Project> CreateNewProject(ProjectMV project)
        {
     
            var result = await httpClient.PostAsJsonAsync("api/project/AddNewProject", project);
            
            var res = await result.Content.ReadFromJsonAsync<Project>();
            return res;
        }

        public async Task DeleteProject(int pid)
        {
       
            var result = await httpClient.GetAsync($"api/project/DeleteProject/{pid}");
        }

        public async Task<List<Project>> GetAllProjectsByWorkspaceId(int cid)
        {
     
            var result = await httpClient.GetFromJsonAsync<List<Project>>($"api/project/getallprojectsbyWorkspaceId/{cid}");
            return result;
        }

        public async Task<Project> GetProjectById(int pid)
        {
           
            var result = await httpClient.GetFromJsonAsync<Project>($"api/project/GetProjectById/{pid}");
            return result;
        }

        public async Task<Project> UpdateProject(Project project)
        {
           
            var result = await httpClient.PostAsJsonAsync($"api/project/UpdateProject", project);
            var res = await result.Content.ReadFromJsonAsync<Project>();
            return res;
        }
    }
}
