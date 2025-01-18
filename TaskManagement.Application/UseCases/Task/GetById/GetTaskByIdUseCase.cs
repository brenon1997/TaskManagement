using TaskManagement.Application.Services;
using TaskManagement.Communication.Responses;

namespace TaskManagement.Application.UseCases.Task.GetById;
public class GetTaskByIdUseCase
{
    private readonly TaskService _taskService;
    public GetTaskByIdUseCase(TaskService taskService)
    {
        _taskService = taskService;
    }
    public ResponseTaskJson? Execute(int id)
    {
        var task = _taskService.GetById(id);

        if (task is null)
        {
            return null;
        }

        return new ResponseTaskJson
        {
            Id = task.Id,
            Name = task.Name,
            Description = task.Description,
            Priority = task.Priority,
            DueDate = task.DueDate,
            Status = task.Status
        };
    }
}
