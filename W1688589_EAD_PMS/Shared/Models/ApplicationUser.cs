using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Shared.Models
{
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("Project")]
        public int? ProjectId { get; set; }

        [ForeignKey("Ticket")]
        public int? TicketId { get; set; }          
    }
}