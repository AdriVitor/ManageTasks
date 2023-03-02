using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ManageTasks.Data;
using ManageTasks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ManageTasks.Settings;

namespace ManageTasks.Services;

public class UserService
{
    private readonly AppDbContext _appDbContext;
    public UserService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<User> FindUser(int id)
    {
        return await _appDbContext.User.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task InsertUser(User user)
    {
        await _appDbContext.User.AddAsync(user);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateUser(int id, User updatedUser)
    {
        var user = await _appDbContext.User.FirstOrDefaultAsync(u => u.Id == id);

        user.Name = updatedUser.Name;
        user.Age = updatedUser.Age;

        _appDbContext.User.Update(user);
        await _appDbContext.SaveChangesAsync();
    }

    public static User Get(User model){
        var users = new List<User>{
            new(){Email = model.Email, Password = model.Password, Id = model.Id}
        };
        var validacao = users.FirstOrDefault(x=>x.Email == model.Email && x.Password == model.Password && x.Id == model.Id);
        
        return validacao;
    }

    public static string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Settings.Settings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<dynamic> AuthenticateUser(User user)
    {
        var model = Get(user);

        var validacao = _appDbContext.User.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
        
        if ((model.Email == null || model.Password == null) || (model.Email.ToLower() != validacao.Email.ToLower() || model.Password != validacao.Password))
        {
            throw new Exception("Usuário ou senha inválidos");
        }
        
        var token = GenerateToken(model);
        model.Id = validacao.Id;
        return new
        {
            model = model,
            token = token
        };
    }
}