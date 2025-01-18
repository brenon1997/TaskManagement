using TaskManagement.Application.Services;
using TaskManagement.Communication.Responses;

namespace TaskManagement.Application.UseCases.Task.GetAll;
public class GetAllTasksUseCase
{
    private readonly TaskService _taskService;

    public GetAllTasksUseCase(TaskService taskService)
    {
        _taskService = taskService;
    }
    public ResponseAllTasksJson Execute()
    {
        var tasks = _taskService.GetAll();
        var responseTasks = tasks.Select(task => new ResponseShortTaskJson
        {
            Id = task.Id,
            Name = task.Name,
            Priority = task.Priority,
            DueDate = task.DueDate,
            Status = task.Status
        }).ToList();

        return new ResponseAllTasksJson
        {
            Tasks = responseTasks
        };
    }
}
