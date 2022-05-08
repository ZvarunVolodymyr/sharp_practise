using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WebApplication1.models;

namespace db;

public class context: IdentityDbContext<User>  
{

    public DbSet<Certificate> Certificates { get; set; }
    // public DbSet<User> User { get; set; }

    public context(DbContextOptions<context> options) : base(options)  
    {  
    }  
    protected override void OnModelCreating(ModelBuilder builder)  
    {  
        base.OnModelCreating(builder);  
    }
    
}

