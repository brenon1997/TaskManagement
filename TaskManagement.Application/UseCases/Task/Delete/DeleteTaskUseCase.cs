using TaskManagement.Application.Services;

namespace TaskManagement.Application.UseCases.Task.Delete;
public class DeleteTaskUseCase
{
    private readonly TaskService _taskService;
    public DeleteTaskUseCase(TaskService taskService)
    {
        _taskService = taskService;
    }
    public bool Execute(int id)
    {
        return _taskService.Delete(id);
    }
}
