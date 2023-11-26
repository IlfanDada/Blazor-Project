using W1688589_EAD_PMS.Server.Repository.ProjectRepos;
using W1688589_EAD_PMS.Shared.Models;
using W1688589_EAD_PMS.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
    public class ProjectController : ControllerBase
    {
        private IProjectRepository _project_repositories;

        public ProjectController(IProjectRepository repository)
        {
            _project_repositories = repository;
        }
        [HttpGet(("{cid:int}"))]
        [Authorize(Roles = "Admin,SubAdmin,Normal")]
        public async Task<ActionResult> GetAllProjectsByWorkspaceId(int cid)
        {
            try
            {
                return Ok(await _project_repositories.GetAllProjectsByWorkspaceId(cid));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occured, while retrieving the data");
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin,SubAdmin")]
        public async Task<IActionResult> AddNewProject([FromBody] ProjectMV project)
        {
            try
            {
                var result = await _project_repositories.CreateNewProject(project);
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
        public async Task<IActionResult> UpdateProject([FromBody] Project project)
        {
            try
            {
                return Ok(await _project_repositories.UpdateProject(project));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occure during Editing");
            }
        }
        [HttpGet("{pid}")]
        [Authorize(Roles = "Admin,SubAdmin")]
        public async Task<ActionResult> GetProjectById(int pid)
        {
            try
            {
                return Ok(await _project_repositories.GetProjectById(pid));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error occured, while retrieving the data");
            } 
        }
        [HttpGet("{pid}")]
        [Authorize(Roles = "Admin,SubAdmin")]
        public async Task<ActionResult> DeleteProject(int pid)
        {
            try
            {
                var result = await _project_repositories.DeleteProject(pid);
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
