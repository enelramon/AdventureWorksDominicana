using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Data.Context;


public class SecurityContext : IdentityDbContext<IdentityUser>
{
    public SecurityContext(DbContextOptions<SecurityContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

    }
}