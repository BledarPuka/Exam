#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace CSharpExamBledar.Models;
public class MyContext : DbContext 
{    
    public MyContext(DbContextOptions options) : base(options) { }    
     
    public DbSet<User> Users {get;set;}
    public DbSet<Project> Projects {get;set;}
    public DbSet<Support> Supports {get;set;}
    
}