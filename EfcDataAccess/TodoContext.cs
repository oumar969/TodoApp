using System.Data.Common;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class TodoContext : DbContext
{
    public DbSet<User>Users { get; set; }
    public DbSet<Todo> Todos { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/Todo.db");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {//Configuring the tables
        modelBuilder.Entity<Todo>().HasKey(todo => todo.Id);
        modelBuilder.Entity<Todo>().Property(todo => todo.Title).HasMaxLength(50);
        modelBuilder.Entity<User>().HasKey(user => user.Id);
    }
    
}