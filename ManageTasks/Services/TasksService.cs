using ManageTasks.Data;
using ManageTasks.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageTasks.Services;

public class TasksService {
    private readonly AppDbContext _appDbContext;
    public TasksService(AppDbContext appDbContext)
    {   
        _appDbContext = appDbContext;
    }

    public async Task<List<Tasks>> FindTasksByIdUser(int userId){
        
        var query = await (from u in _appDbContext.User
                    join t in _appDbContext.Tasks
                    on u.Id equals t.UserId
                    where t.UserId == userId
                    select new{
                        Tasks = _appDbContext.Tasks.Where(t=>t.UserId == userId).ToList()
                    }).FirstOrDefaultAsync();

        if(query?.Tasks == null){
            return new List<Tasks>();
        }

        return query.Tasks;     
    }

    public async Task InsertTask(Tasks task){
        
        if(task.DateTaskValid(task.Date) == false){
            throw new Exception("A data da tarefa não pode ser anterior a data atual e nem maior do que 2 anos");
        }

        await _appDbContext.Tasks.AddAsync(task);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateTask(int userId, int taskId,Tasks updatedTask){
        
        var task = await _appDbContext.Tasks.FirstOrDefaultAsync(t=>t.Id == taskId && t.UserId == userId);
        if(task == null){
            throw new Exception("A tarefa não foi encontrada");
        }

        task.Title = updatedTask.Title;
        task.Description = updatedTask.Description;
        task.Date = updatedTask.Date;
        task.Status = updatedTask.Status;

        _appDbContext.Tasks.Update(task);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteTask(int userId, int taskId){

        var task = await _appDbContext.Tasks.FirstOrDefaultAsync(t=>t.Id == taskId && t.UserId == userId);
        if(task == null){
            throw new Exception("A tarefa não foi encontrada");
        }

        _appDbContext.Tasks.Remove(task);
        await _appDbContext.SaveChangesAsync();
    }
}