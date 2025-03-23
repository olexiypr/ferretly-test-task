using Ferretly.TestTask.TimeTrackingApi.RequestModels;
using Ferretly.TestTask.TimeTrackingApi.ResponseModels;

namespace Ferretly.TestTask.TimeTrackingApi.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseModel>> GetAll();
    Task<ProjectResponseModel> GetById(int id);
    Task<bool> CreateProject(ProjectRequestModel requestModel);
    Task<ProjectResponseModel> UpdateProject(int id, ProjectRequestModel requestModel);
    Task<bool> DeleteById(int id);
}