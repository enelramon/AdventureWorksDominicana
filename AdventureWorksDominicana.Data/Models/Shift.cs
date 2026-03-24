using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Data.Models;

/// <summary>
/// Work shift lookup table.
/// </summary>
[Table("Shift", Schema = "HumanResources")]
[Index("Name", Name = "AK_Shift_Name", IsUnique = true)]
[Index("StartTime", "EndTime", Name = "AK_Shift_StartTime_EndTime", IsUnique = true)]
public partial class Shift
{
    /// <summary>
    /// Primary key for Shift records.
    /// </summary>
    [Key]
    [Column("ShiftID")]
    public byte ShiftId { get; set; }

    /// <summary>
    /// Shift description.
    /// </summary>
    [Required(ErrorMessage = "El nombre del turno es obligatorio.")]
    [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Shift start time.
    /// </summary>
    [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
    public TimeOnly StartTime { get; set; }

    /// <summary>
    /// Shift end time.
    /// </summary>
    [Required(ErrorMessage = "La hora de fin es obligatoria.")]
    public TimeOnly EndTime { get; set; }

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    [InverseProperty("Shift")]
    public virtual ICollection<EmployeeDepartmentHistory> EmployeeDepartmentHistories { get; set; } = new List<EmployeeDepartmentHistory>();
}
