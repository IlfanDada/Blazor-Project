using W1688589_EAD_PMS.Shared.Models;
using W1688589_EAD_PMS.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Server.Repository.UserRepos
{
    public interface IUserRepository
    {
        Task<List<ReadUserMV>> GetAllUserByWorkspaceID(int cid);
        Task<string> DeleteUser(string uid);
        Task<string> UpdateUser(UpdateUserMV users);
        Task<string> CreateNewUser(UserMV users);
        Task<List<ApplicationUser>> GetAllUserByPid(int pid);
        Task<List<ReadUserMV>> SearchUsers(int cid, string email);
        Task<string> ChangeUserRole(string uid, string role);
        Task<ApplicationUser> GetUserById(string uid);

    }
}
