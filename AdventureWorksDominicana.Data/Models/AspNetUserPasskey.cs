using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Data.Models;

[Index("UserId", Name = "IX_AspNetUserPasskeys_UserId")]
public partial class AspNetUserPasskey
{
    [Key]
    [MaxLength(1024)]
    public byte[] CredentialId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Data { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("AspNetUserPasskeys")]
    public virtual AspNetUser User { get; set; } = null!;
}
