using Ferretly.TestTask.TimeTrackingApi.RequestModels;
using Ferretly.TestTask.TimeTrackingApi.ResponseModels;
using Ferretly.TestTask.TimeTrackingApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ferretly.TestTask.TimeTrackingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController(IProjectService projectService) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<ProjectResponseModel>> GetAll()
    {
        return await projectService.GetAll();
    }

    [HttpGet("{id:int}")]
    public async Task<ProjectResponseModel> GetById(int id)
    {
        return await projectService.GetById(id);
    }

    [HttpPost]
    public async Task CreateProject([FromBody] ProjectRequestModel projectRequestModel)
    {
        await projectService.CreateProject(projectRequestModel);
    }

    [HttpPut("{id:int}")]
    public async Task UpdateProject(int id, [FromBody] ProjectRequestModel projectRequestModel)
    {
        await projectService.UpdateProject(id, projectRequestModel);
    }

    [HttpDelete]
    public async Task DeleteProjectById(int id)
    {
        await projectService.DeleteById(id);
    }
}