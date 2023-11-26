using W1688589_EAD_PMS.Server.Repository.WorkspaceRepos;
using W1688589_EAD_PMS.Shared.Models;
using W1688589_EAD_PMS.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WorkspaceController : ControllerBase
    {
        private IWorkspaceRepository _workspace_repositories;

        public WorkspaceController(IWorkspaceRepository repository)
        {
            _workspace_repositories = repository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SubAdmin,Normal")]
        public async Task<ActionResult<List<Workspace>>> GetAllCompaniesByUserid()
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                return Ok(await _workspace_repositories.GetAllWorkspacesByUserId(userId));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occured, while retrieving the data");
            }
        }

        [HttpGet("{cid}")]
        [Authorize(Roles = "Admin,SubAdmin")]
        public async Task<ActionResult> GetWorkspaceById(int cid)
        {
            try
            {
                return Ok(await _workspace_repositories.GetWorkspaceById(cid));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occured, while retrieving the data");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SubAdmin")]
        public async Task<IActionResult> AddNewWorkspace([FromBody] WorkspaceMV Workspace)
        {
            try
            {
                //getting userId 
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _workspace_repositories.CreateNewWorkspace(Workspace, userId);             
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occured during Saving");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SubAdmin")]
        public async Task<IActionResult> UpdateWorkspace([FromBody] Workspace Workspace)
        {
            try
            {
                return Ok(await _workspace_repositories.UpdateWorkspace(Workspace));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occure during Editing");
            }
        }

        [HttpGet("{cid}")]
        [Authorize(Roles = "Admin,SubAdmin")]
        public async Task<ActionResult> DeleteWorkspace(int cid)
        {
            try
            {
                var result = await _workspace_repositories.DeleteWorkspace(cid);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occured during deleting");
            }
        }
    }
}
