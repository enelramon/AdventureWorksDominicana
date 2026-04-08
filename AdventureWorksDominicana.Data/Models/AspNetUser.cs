using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Data.Models;

[Index("NormalizedEmail", Name = "EmailIndex")]
public partial class AspNetUser : IdentityUser 
{


	[InverseProperty("User")]
	public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

	[InverseProperty("User")]
	public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

	[InverseProperty("User")]
	public virtual ICollection<AspNetUserPasskey> AspNetUserPasskeys { get; set; } = new List<AspNetUserPasskey>();

	[InverseProperty("User")]
	public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

	[ForeignKey("UserId")]
	[InverseProperty("Users")]
	public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}