using ManageTasks.Models;
using ManageTasks.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManageTasks.Controllers;

 [ApiVersion("1")]
 [Route("api/v{version:apiVersion}/[controller]")]
 [ApiController]
public class UserController : ControllerBase{

    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id){
        try
        {
            var user = await _userService.FindUser(id);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> PostUser(User user){
        try
        {
            await _userService.InsertUser(user);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutUser(int id, User user){
        try
        {
            await _userService.UpdateUser(id, user);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
} 