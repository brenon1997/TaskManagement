using TaskManagement.Application.Services;
using TaskManagement.Application.Models;
using TaskManagement.Communication.Requests;
using TaskManagement.Communication.Responses;

namespace TaskManagement.Application.UseCases.Task.Register;
public class RegisterTaskUseCase
{
    private readonly TaskService _taskService;

    public RegisterTaskUseCase(TaskService taskService)
    {
        _taskService = taskService;
    }

    public ResponseRegisterTaskJson Execute(RequestRegisterTaskJson request)
    {
        TaskModel task = new()
        {
            Name = request.Name,
            Description = request.Description,
            Priority = request.Priority,
            DueDate = request.DueDate,
            Status = request.Status
        };

        _taskService.Register(task);

        return new ResponseRegisterTaskJson
        {
            Id = task.Id,
            Name = task.Name
        };
    }
}
