using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PractAssignment.Models;

public class AuthDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly IConfiguration _configuration;

    //public AuthDbContext(DbContextOptions<AuthDbContext> options):base(options){ }
    public AuthDbContext(IConfiguration configuration, DbContextOptions<AuthDbContext> options) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = _configuration.GetConnectionString("AuthConnectionString");
        optionsBuilder.UseSqlServer(connectionString);
    }

    private DbSet<ApplicationUser> User { get; set; }
}