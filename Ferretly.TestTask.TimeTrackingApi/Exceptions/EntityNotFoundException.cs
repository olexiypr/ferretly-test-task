namespace Ferretly.TestTask.TimeTrackingApi.Exceptions;

public class EntityNotFoundException(string entityName, int id) : Exception($"{entityName} with id {id} not found")
{
    
}