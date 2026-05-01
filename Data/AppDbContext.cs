using Microsoft.EntityFrameworkCore;
using Sms.Models;

namespace Sms.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Students> Students { get; set; }
    public DbSet<Classes> Classes { get; set; }
}