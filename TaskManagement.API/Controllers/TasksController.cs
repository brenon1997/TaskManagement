using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.UseCases.Task.Register;
using TaskManagement.Communication.Requests;
using TaskManagement.Communication.Responses;

namespace TaskManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly RegisterTaskUseCase _registerTaskUseCase;

    public TasksController(RegisterTaskUseCase registerTaskUseCase)
    {
        _registerTaskUseCase = registerTaskUseCase;
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
}
