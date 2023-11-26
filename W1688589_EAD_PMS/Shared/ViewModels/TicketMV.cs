using W1688589_EAD_PMS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Shared.ViewModels
{
    public class TicketMV //VIEW MODEL FOR THE FRONT END + REPOS
    {
        public int TicketId { get; set; } 
        public string TicketName { get; set; } 
        public string Card { get; set; } // For Modal btns
        public List<ApplicationUser> UserList { get; set; } // for authorized users
        public int ProjectId { get; set; }
        //  public int AssignTicketToUser { get; set; }
    }
}
