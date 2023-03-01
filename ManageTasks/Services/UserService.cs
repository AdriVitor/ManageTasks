using ManageTasks.Data;
using ManageTasks.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageTasks.Services;

public class UserService{
    private readonly AppDbContext _appDbContext;
    public UserService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<User> FindUser(int id){
        return await _appDbContext.User.FirstOrDefaultAsync(u=>u.Id == id);
    }

    public async Task InsertUser(User user){
        await _appDbContext.User.AddAsync(user);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateUser(int id, User updatedUser){
        var user = await _appDbContext.User.FirstOrDefaultAsync(u=>u.Id == id);
        
        user.Name = updatedUser.Name;
        user.Age = updatedUser.Age;
        
        _appDbContext.User.Update(user);
        await _appDbContext.SaveChangesAsync();
    }
}