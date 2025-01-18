using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.UseCases.Task.GetAll;
using TaskManagement.Application.UseCases.Task.GetById;
using TaskManagement.Application.UseCases.Task.Register;
using TaskManagement.Application.UseCases.Task.Update;
using TaskManagement.Communication.Requests;
using TaskManagement.Communication.Responses;

namespace TaskManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly RegisterTaskUseCase _registerTaskUseCase;
    private readonly GetAllTasksUseCase _getAllTasksUseCase;
    private readonly GetTaskByIdUseCase _getTaskByIdUseCase;
    private readonly UpdateTaskUseCase _updateTaskUseCase;

    public TasksController(RegisterTaskUseCase registerTaskUseCase, GetAllTasksUseCase getAllTasksUseCase, GetTaskByIdUseCase getTaskByIdUseCase, UpdateTaskUseCase updateTaskUseCase)
    {
        _registerTaskUseCase = registerTaskUseCase;
        _getAllTasksUseCase = getAllTasksUseCase;
        _getTaskByIdUseCase = getTaskByIdUseCase;
        _updateTaskUseCase = updateTaskUseCase;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterTaskJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public IActionResult RegisterTask([FromBody] RequestTaskJson request)
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

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseTaskJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult Get([FromRoute] int id)
    {
        ResponseTaskJson? response = _getTaskByIdUseCase.Execute(id);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseUpdateTaskJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public IActionResult Update([FromRoute] int id, [FromBody] RequestTaskJson request)
    {
        if (request == null)
        {
            return BadRequest();
        }
        ResponseUpdateTaskJson? response = _updateTaskUseCase.Execute(id, request);
        if (response == null)
        {
            return NotFound();
        }
        return Ok(response);
    }
}
