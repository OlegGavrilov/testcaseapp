using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TestCaseApp;

public class TestCaseAppUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
}


public class UsersContext : IdentityUserContext<TestCaseAppUser>
{
    public UsersContext (DbContextOptions<UsersContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // It would be a good idea to move the connection string to user secrets
        options.UseSqlServer("Server=localhost,1433;Initial Catalog=TestCaseAppDb;Integrated Security=False;User Id=sa;Password=P@assWord;TrustServerCertificate=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestCaseAppUser>()
            .HasIndex(p => new { p.Phone })
            .IsUnique();
        
        modelBuilder.Entity<TestCaseAppUser>()
            .HasIndex(p => new { p.Email })
            .IsUnique();
        
        base.OnModelCreating(modelBuilder);
    }
}