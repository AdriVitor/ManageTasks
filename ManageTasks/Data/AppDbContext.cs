using ManageTasks.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageTasks.Data;

public class AppDbContext : DbContext{
    public DbSet<User> User { get; set; }
    public DbSet<Tasks> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options){
        options.UseSqlServer(@"Server=DESKTOP-H7797U1\SQLEXPRESS;
                             Database=MANAGETASKS;
                             Integrated Security=True;
                             Encrypt=False");
    }

    protected void OnModelConfiguring(ModelBuilder builder){
            builder.Entity<User>().ToTable("User");
            builder.Entity<Tasks>().ToTable("Tasks");
    }
}