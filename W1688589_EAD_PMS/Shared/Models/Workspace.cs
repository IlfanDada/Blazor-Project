using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Shared.Models
{
    public class Workspace //WORKSPACE TABLE
    {
        [Key]
        public int WorkspaceId { get; set; } //MAKE SURE THIS IS PK OR ELSE
        public string WorkspaceName { get; set; }
        public string WorkspaceDescription { get; set; }
    }
}
