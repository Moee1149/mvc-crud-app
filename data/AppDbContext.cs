using Microsoft.EntityFrameworkCore;
using MyMvcApp.Models; // Namespace where your models live

namespace MyMvcApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Example table
    public DbSet<Employee> Employees { get; set; }
}
