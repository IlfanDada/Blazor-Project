using W1688589_EAD_PMS.Shared.Models;
using W1688589_EAD_PMS.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Client.Services.Members
{
   public interface IUserService
    {
        Task<string> CreateNewUser(UserMV users);
        Task<List<ReadUserMV>> GetAllWorkspaceUser(int cid);
        Task ChangeRole(string uid, string role);
        Task<ApplicationUser> GetUserById(string uid);
        Task DeleteUser(string uid);
        Task<List<ReadUserMV>> SearchUsers(int cid, string email);
        
        Task<string> UpdateUser(UpdateUserMV users);
        Task<List<ApplicationUser>> GetAllUserByPid(int pid);
    }
}
