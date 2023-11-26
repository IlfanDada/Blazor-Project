using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Shared.ViewModels
{
    public class UserMV  //VIEW MODEL FOR THE FRONT END 
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int? ProjectId { get; set; }
        public int? WorkspaceId { get; set; }
    }
}
