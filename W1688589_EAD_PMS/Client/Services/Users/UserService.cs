using W1688589_EAD_PMS.Shared.Models;
using W1688589_EAD_PMS.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Client.Services.Members
{
    public class UserService : IUserService
    {
        //to make http calls to send and receive data from an API.
        //In both the hosting models, that is Blazor WebAssembly and Blazor Server we use this
        //same HttpClient class
        private readonly HttpClient httpClient;
        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task ChangeRole(string uid, string role)
        {
        
            await httpClient.GetAsync($"api/users/ChangeUserRole/{uid}/{role}");
        }

        public async Task<string> CreateNewUser(UserMV users)
        {
           
            var result = await httpClient.PostAsJsonAsync("api/users/AddNewMember", users);
         
            var res = await result.Content.ReadAsStringAsync();
            return res;

        }

        public async Task DeleteUser(string uid)
        {
           
            var result = await httpClient.GetAsync($"api/users/DeleteUser/{uid}");
        }

        public async Task<List<ReadUserMV>> GetAllWorkspaceUser(int cid)
        {
           
            var result = await httpClient.GetFromJsonAsync<List<ReadUserMV>>($"api/users/GetAllWorkspaceUser/{cid}");
            return result;
        }

        public async Task<List<ReadUserMV>> SearchUsers(int cid, string email)
        {
            List<ReadUserMV> result = new();
        
            if (string.IsNullOrEmpty(email))
            {
      
                result = await httpClient.GetFromJsonAsync<List<ReadUserMV>>($"api/users/GetAllWorkspaceUser/{cid}");
                return result;
            }
            else
            {
                
                result = await httpClient.GetFromJsonAsync<List<ReadUserMV>>($"api/users/SearchUsers/{cid}/{email}");
                return result;
            }
        }

        public async Task<ApplicationUser> GetUserById(string uid)
        {
          
            var result = await httpClient.GetFromJsonAsync<ApplicationUser>($"api/users/GetUserById/{uid}");
            return result;
        }

       

        public async Task<string> UpdateUser(UpdateUserMV users)
        {
           
            var result = await httpClient.PostAsJsonAsync("api/users/UpdateUser", users);
            var res = await result.Content.ReadAsStringAsync();
            return res;
        }

        public async Task<List<ApplicationUser>> GetAllUserByPid(int pid)
        {
            var result = await httpClient.GetFromJsonAsync<List<ApplicationUser>>($"api/users/GetAllUserByPid/{pid}");
            return result;
        }
    }
}
