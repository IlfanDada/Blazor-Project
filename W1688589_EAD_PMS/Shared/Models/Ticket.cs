using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Shared.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; } //MAKE SURE THIS IS PK OR ELSE
        public string TicketName { get; set; }
        public string Card { get; set; }
        [ForeignKey("Project")] //MAKE SURE THIS IS FK TO NEST THE TICKET IDs 
        public int ProjectId { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

    }
}
