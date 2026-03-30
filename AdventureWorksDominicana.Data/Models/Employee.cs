using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Data.Models;

/// <summary>
/// Employee information such as salary, department, and title.
/// </summary>
[Table("Employee", Schema = "HumanResources")]
[Index("LoginId", Name = "AK_Employee_LoginID", IsUnique = true)]
[Index("NationalIdnumber", Name = "AK_Employee_NationalIDNumber", IsUnique = true)]
[Index("Rowguid", Name = "AK_Employee_rowguid", IsUnique = true)]
public partial class Employee
{
    /// <summary>
    /// Primary key for Employee records.  Foreign key to BusinessEntity.BusinessEntityID.
    /// </summary>
    [Key]
    [Column("BusinessEntityID")]
    [Required(ErrorMessage = "El ID de la entidad es obligatorio.")]
    public int BusinessEntityId { get; set; }

    /// <summary>
    /// Unique national identification number such as a social security number.
    /// </summary>
    [Column("NationalIDNumber")]
    [Required(ErrorMessage = "El número de identificación nacional es obligatorio.")]
    [StringLength(15, ErrorMessage = "El número de identificación no puede exceder los 15 caracteres.")]
    public string NationalIdnumber { get; set; } = null!;

    /// <summary>
    /// Network login.
    /// </summary>
    [Column("LoginID")]
    [Required(ErrorMessage = "El LoginID es obligatorio.")]
    [StringLength(256, ErrorMessage = "El LoginID no puede exceder los 256 caracteres.")]
    public string LoginId { get; set; } = null!;

    /// <summary>
    /// The depth of the employee in the corporate hierarchy.
    /// </summary>
    public short? OrganizationLevel { get; set; }

    /// <summary>
    /// Work title such as Buyer or Sales Representative.
    /// </summary>
    public string JobTitle { get; set; } = null!;

    /// <summary>
    /// Date of birth.
    /// </summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// M = Married, S = Single
    /// </summary>
    public string MaritalStatus { get; set; } = null!;

    /// <summary>
    /// M = Male, F = Female
    /// </summary>
    public string Gender { get; set; } = null!;

    /// <summary>
    /// Employee hired on this date.
    /// </summary>
    public DateOnly HireDate { get; set; }

    /// <summary>
    /// Job classification. 0 = Hourly, not exempt from collective bargaining. 1 = Salaried, exempt from collective bargaining.
    /// </summary>
    public bool SalariedFlag { get; set; }

    /// <summary>
    /// Number of available vacation hours.
    /// </summary>
    public short VacationHours { get; set; }

    /// <summary>
    /// Number of available sick leave hours.
    /// </summary>
    public short SickLeaveHours { get; set; }

    /// <summary>
    /// 0 = Inactive, 1 = Active
    /// </summary>
    public bool CurrentFlag { get; set; }

    /// <summary>
    /// ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
    /// </summary>
    [Column("rowguid")]
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    [ForeignKey("BusinessEntityId")]
    [InverseProperty("Employee")]
    public virtual Person BusinessEntity { get; set; } = null!;

    [InverseProperty("BusinessEntity")]
    public virtual ICollection<EmployeeDepartmentHistory> EmployeeDepartmentHistories { get; set; } = new List<EmployeeDepartmentHistory>();

    [InverseProperty("BusinessEntity")]
    public virtual ICollection<EmployeePayHistory> EmployeePayHistories { get; set; } = new List<EmployeePayHistory>();

    [InverseProperty("BusinessEntity")]
    public virtual ICollection<JobCandidate> JobCandidates { get; set; } = new List<JobCandidate>();

    [InverseProperty("Employee")]
    public virtual ICollection<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; } = new List<PurchaseOrderHeader>();

    [InverseProperty("BusinessEntity")]
    public virtual SalesPerson? SalesPerson { get; set; }
}