using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Shared.ViewModels //VIEW MODEL FOR THE FRONT END + REPOS
{
    public class ReadUserMV
    {
        public string UserId { get; set; }  //required for creating the intial admin and used to parse the id to find users in the CRUD > User manager
        public string Email { get; set; } //Used in the User Manager Page
        public int? TicketId { get; set; } //Used in the ticket repos which is connected the add ticket btn
        public string TicketName { get; set; } //view created ticket in ticket page
        public string ProjectName { get; set; } //used to allow users to change the tickets displayed in the ticket page using the form control
        public string Role { get; set; } //Only Authorized users can see certain pages/ have access to certain btns 
        
    }
}
