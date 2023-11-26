using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Shared.ViewModels
{
   public class UpdateTicketMV //VIEW MODEL FOR THE FRONT END + REPOS
    {
        public int TicketId { get; set; }
        public string TicketName { get; set; }
        public List<string> UserList { get; set; }
    }
}
