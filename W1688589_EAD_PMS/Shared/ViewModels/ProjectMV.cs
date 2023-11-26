using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Shared.ViewModels //VIEW MODEL FOR THE FRONT END + REPOS
{
    public class ProjectMV
    {
        public string ProjectName { get; set; } // The name that the admin assigns to the project
        public int? WorkspaceId { get; set; } //links to it's respectful workspace
    }
}
