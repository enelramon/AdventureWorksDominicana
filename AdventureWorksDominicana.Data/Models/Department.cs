using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace AdventureWorksDominicana.Data.Models;
[Table("Department", Schema = "HumanResources")]
[Index("Name", Name = "AK_Department_Name", IsUnique = true)]
public partial class Department
{
    [Key]
    [Column("DepartmentID")]
    public short DepartmentId { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Campo Obligatorio")]
    [StringLength(50)]
    public string GroupName { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; } = DateTime.Now;

    [InverseProperty("Department")]
    public virtual ICollection<EmployeeDepartmentHistory> EmployeeDepartmentHistories { get; set; } = new List<EmployeeDepartmentHistory>();
}
