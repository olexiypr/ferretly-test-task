using Ferretly.TestTask.TimeTrackingApi.RequestModels;
using Ferretly.TestTask.TimeTrackingApi.ResponseModels;
using Ferretly.TestTask.TimeTrackingApi.Services.Interfaces;

namespace Ferretly.TestTask.TimeTrackingApi.Services.Implementations;

public class ProjectService : IProjectService
{
    //Keep in mind, we don't delete projects, we set IsDeleted to true.
    //So we have to check if project is deleted or not when we get it form db
    public Task<IEnumerable<ProjectResponseModel>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<ProjectResponseModel> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateProject(ProjectRequestModel requestModel)
    {
        //Should be checked that end date > current date
        throw new NotImplementedException();
    }

    public Task<ProjectResponseModel> UpdateProject(int id, ProjectRequestModel requestModel)
    {
        //Should be checked that end date > current date
        throw new NotImplementedException();
    }

    public Task<bool> DeleteById(int id)
    {
        throw new NotImplementedException();
    }
}