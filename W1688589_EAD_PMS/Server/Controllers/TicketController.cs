using W1688589_EAD_PMS.Server.Repository.TicketRepository;
using W1688589_EAD_PMS.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private const string V = "Admin,SubAdmin,Normal";
        private ITicketRepository _ticket_repositories;

        public TicketController(ITicketRepository repository)
        {
            _ticket_repositories = repository;
        }
        [HttpGet(("{pid:int}"))]
        [Authorize(Roles = V)]
        public async Task<ActionResult> GetAllTicketbyProjectId(int pid)
        {
            try
            {
                return Ok(await _ticket_repositories.GetAllTicketbyProjectId(pid));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occured, while retrieving the data");
            }
        }
        [HttpGet(("{pid:int}"))]
        [Authorize(Roles = V)]
        public async Task<ActionResult> AreAllTicketsCompleted(int pid)
        {
            try
            {
                return Ok(await _ticket_repositories.AreAllTicketsCompleted(pid));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occured, while retrieving the data");
            }
        }
        [HttpGet("{uid}")]
        [Authorize(Roles = V)]
        public async Task<ActionResult> IsTicketAssigned(string uid)
        {
            try
            {
                return Ok(await _ticket_repositories.IsTicketAssigned(uid));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occured, while retrieving the data");
            }
        }
        [HttpGet("{tid}")]
        [Authorize(Roles = V)]
        public async Task<ActionResult> DeleteTicket(int tid)
        {
            try
            {
                return Ok(await _ticket_repositories.DeleteTicket(tid));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occured, while retrieving the data");
            }
        }

        [HttpGet("{tid}")]
        [Authorize(Roles = V)]
        public async Task<ActionResult> GetTicketById(int tid)
        {
            try
            {
                var result = await _ticket_repositories.GetTicketById(tid);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occured, while retrieving the data");
            }
        }

        [HttpPost]
        [Authorize(Roles = V)]
        public async Task<IActionResult> UpdateTicket([FromBody] UpdateTicketMV ticket)
        {
            try
            {
                return Ok(await _ticket_repositories.UpdateTicket(ticket));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occure during Editing");
            }
        }
        [HttpPost]
        [Authorize(Roles = V)]
        public async Task<IActionResult> AddTickets([FromBody] CreateTicketMV ticket)
        {
            try
            {
                return Ok(await _ticket_repositories.CreateNewTicket(ticket));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occure during Adding");
            }
        }

        [HttpGet("{pid:int}/{email}")]
        [Authorize(Roles = "Admin,SubAdmin")]
        public async Task<ActionResult> GetAllUsersbyProjectId(int pid, string email)
        {
            try
            {
                return Ok(await _ticket_repositories.GetAllUserbyProjectId(pid, email));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occured, while retrieving the data");
            }
        }
    }
}
