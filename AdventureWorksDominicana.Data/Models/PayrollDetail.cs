using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorksDominicana.Data.Models;

[Table("PayrollDetail", Schema = "HumanResources")]
public partial class PayrollDetail
{
    [Key]
    public int PayrollDetailId { get; set; }

    [Required]
    public int PayrollId { get; set; }

    [Required]
    [Column("BusinessEntityID")]
    public int BusinessEntityId { get; set; }

    [Column(TypeName = "money")]
    public decimal GrossSalary { get; set; } // Salario Bruto Generado

    // DEDUCCIONES AL EMPLEADO 
    [Column(TypeName = "money")]
    public decimal SfsDeduction { get; set; }

    [Column(TypeName = "money")]
    public decimal AfpDeduction { get; set; }

    [Column(TypeName = "money")]
    public decimal IsrDeduction { get; set; }

    [Column(TypeName = "money")]
    public decimal OtherDeductions { get; set; }

    [Column(TypeName = "money")]
    public decimal NetSalary { get; set; } // Lo que el empleado cobra en el banco

    [ForeignKey("PayrollId")]
    [InverseProperty("PayrollDetails")]
    public virtual Payroll Payroll { get; set; } = null!;

    [ForeignKey("BusinessEntityId")]
    [InverseProperty("PayrollDetails")]
    public virtual Employee Employee { get; set; } = null!;
}