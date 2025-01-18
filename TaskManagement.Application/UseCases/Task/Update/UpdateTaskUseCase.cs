using TaskManagement.Application.Services;
using TaskManagement.Communication.Requests;
using TaskManagement.Communication.Responses;

namespace TaskManagement.Application.UseCases.Task.Update;
public class UpdateTaskUseCase
{
    private readonly TaskService _taskService;
    public UpdateTaskUseCase(TaskService taskService)
    {
        _taskService = taskService;
    }
    public ResponseUpdateTaskJson? Execute(int id, RequestTaskJson request)
    {
        var task = _taskService.GetById(id);
        if (task is null)
        {
            return null;
        }
        task.Name = request.Name;
        task.Description = request.Description;
        task.Priority = request.Priority;
        task.DueDate = request.DueDate;
        task.Status = request.Status;
        _taskService.Update(task);
        return new ResponseUpdateTaskJson
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
