using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.UseCases.Task.GetAll;
using TaskManagement.Application.UseCases.Task.Register;
using TaskManagement.Communication.Requests;
using TaskManagement.Communication.Responses;

namespace TaskManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly RegisterTaskUseCase _registerTaskUseCase;
    private readonly GetAllTasksUseCase _getAllTasksUseCase;

    public TasksController(RegisterTaskUseCase registerTaskUseCase, GetAllTasksUseCase getAllTasksUseCase)
    {
        _registerTaskUseCase = registerTaskUseCase;
        _getAllTasksUseCase = getAllTasksUseCase;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterTaskJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public IActionResult RegisterTask([FromBody] RequestRegisterTaskJson request)
    {
        if (request == null)
        {
            return BadRequest(new ResponseErrorsJson { Errors = new List<string> { "Invalid request" } });
        }

        ResponseRegisterTaskJson response = _registerTaskUseCase.Execute(request);
        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseAllTasksJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetAll()
    {
        ResponseAllTasksJson response = _getAllTasksUseCase.Execute();

        if (!response.Tasks.Any())
        {
            return NoContent();
        }

        return Ok(response);
    }
}
