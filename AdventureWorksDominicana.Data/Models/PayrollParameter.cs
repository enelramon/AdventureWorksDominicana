using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorksDominicana.Data.Models;

[Table("PayrollParameter", Schema = "HumanResources")]
public partial class PayrollParameter
{
    [Key]
    public int PayrollParameterId { get; set; }

    public bool IsActive { get; set; } // true = Es la ley actual, false = Ley vieja

    //  LO QUE SE LE DESCUENTA AL EMPLEADO
    [Column(TypeName = "decimal(5, 4)")]
    public decimal SfsPct { get; set; } //  (Seguro de Salud)

    [Column(TypeName = "decimal(5, 4)")]
    public decimal AfpPct { get; set; } // (Fondo de Pensión)

    //  TOPES PARA QUE LA MATEMÁTICA NO FALLE
    [Column(TypeName = "money")]
    public decimal MinimumWage { get; set; } // Salario Mínimo

    [Column(TypeName = "money")]
    public decimal IsrAnnualExemption { get; set; } // Tope exento de impuestos DGII
}