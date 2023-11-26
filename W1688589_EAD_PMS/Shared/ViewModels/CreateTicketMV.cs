using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Shared.ViewModels
{       
    //creates ticket
    public class CreateTicketMV //VIEW MODEL FOR THE FRONT END + REPOS
    {
        public string TicketName { get; set; }
        public string Card { get; set; } //connects to the ticket card in the Ticket Page
        public List<string> UserList { get; set; }
        public int ProjectId { get; set; } //connects to the project id - only tickets in the assigned project will be views
    }
}
