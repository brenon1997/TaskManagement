using TaskManagement.Application.Services;
using TaskManagement.Application.UseCases.Task.GetAll;
using TaskManagement.Application.UseCases.Task.GetById;
using TaskManagement.Application.UseCases.Task.Register;
using TaskManagement.Application.UseCases.Task.Update;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<TaskService>();
builder.Services.AddTransient<RegisterTaskUseCase>();
builder.Services.AddTransient<GetAllTasksUseCase>();
builder.Services.AddTransient<GetTaskByIdUseCase>();
builder.Services.AddTransient<UpdateTaskUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
