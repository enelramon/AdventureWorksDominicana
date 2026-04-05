using AdventureWorksDominicana.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Data.Context;


public class SecurityContext : IdentityDbContext<AspNetUser>
{
    public SecurityContext(DbContextOptions<SecurityContext> options)
        : base(options)
    {
    }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			
			optionsBuilder.UseSqlServer("workstation id=AdventureWorksDb.mssql.somee.com;packet size=4096;user id=AdenawellTorres_SQLLogin_1;pwd=tc54sf6glk;data source=AdventureWorksDb.mssql.somee.com;persist security info=False;initial catalog=AdventureWorksDb;TrustServerCertificate=True");
		}
	}

	protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

    }
}