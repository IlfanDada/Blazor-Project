using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Shared.Models
{
   public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public bool Completed { get; set; }
        public DateTime? EndDate { get; set; }
        [ForeignKey("Workspace")] //MAKE SURE THIS IS FK TO NEST THE PROJECTS IDs 
        public int? WorkspaceId { get; set; }
        public Workspace Workspace { get; set; }
    }
}
