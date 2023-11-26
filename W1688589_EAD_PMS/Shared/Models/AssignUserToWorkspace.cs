using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Shared.Models
{
    public class AssignUserToWorkspace
    {
        [Key]
        public int WorkspaceUserId { get; set; }
        [ForeignKey("Workspace")]
        public int WorkspaceId { get; set; }
        public Workspace Workspace { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
    }
}
