using ManageTasks.Models;
using ManageTasks.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManageTasks.Controllers;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class TasksController : ControllerBase{
    private readonly TasksService _tasksService;

    public TasksController(TasksService tasksService)
    {
        _tasksService = tasksService;
    }    

    [HttpGet("{userId}")]
    public async Task<ActionResult<List<Tasks>>> GetTasksByUserId(int userId){
        try
        {
            var tasks = await _tasksService.FindTasksByIdUser(userId);
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> PostTask(Tasks task){
        try
        {
            await _tasksService.InsertTask(task);
            return Ok();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpPut("{userId}/{taskId}")]
    public async Task<ActionResult> PutTask(int userId, int taskId,Tasks updatedTask){
        try
        {
            await _tasksService.UpdateTask(userId, taskId, updatedTask);
            return Ok();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpDelete("{userId}/{taskId}")]
    public async Task<ActionResult> DeleteTask(int userId, int taskId){
        try
        {
            await _tasksService.DeleteTask(userId, taskId);
            return Ok();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}