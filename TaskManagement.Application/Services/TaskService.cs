using TaskManagement.Application.Models;

namespace TaskManagement.Application.Services;
public class TaskService
{
    private readonly List<TaskModel> _tasks = [];
    private int _lastId = 0;

    public TaskModel Register(TaskModel task)
    {
        task.Id = ++_lastId;
        _tasks.Add(task);
        return task;
    }
    public List<TaskModel> GetAll()
    {
        return _tasks;
    }

    public TaskModel? GetById(int id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }
}
