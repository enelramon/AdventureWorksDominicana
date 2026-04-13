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
    [Required(ErrorMessage = "El porcentaje de SFS es obligatorio")]
    [Range(0.01, 0.10, ErrorMessage = "El SFS debe estar entre 1% (0.01) y 10% (0.10)")]
    public decimal SfsPct { get; set; } //  (Seguro de Salud)

    [Column(TypeName = "decimal(5, 4)")]
    [Required(ErrorMessage = "El porcentaje de AFP es obligatorio")]
    [Range(0.01, 0.10, ErrorMessage = "El AFP debe estar entre 1% (0.01) y 10% (0.10)")]
    public decimal AfpPct { get; set; } // (Fondo de Pensión)

    //  TOPES PARA QUE LA MATEMÁTICA NO FALLE
    [Required(ErrorMessage = "El sueldo mínimo es necesario para los topes")]
    [Range(1000, 100000, ErrorMessage = "Monto de sueldo mínimo no válido")]
    [Column(TypeName = "money")]
    public decimal MinimumWage { get; set; } // Salario Mínimo

    [Column(TypeName = "money")]
    [Required(ErrorMessage = "La exención de ISR es obligatoria")]
    [Range(0, 2000000, ErrorMessage = "Monto de exención anual fuera de rango")]
    public decimal IsrAnnualExemption { get; set; } // Tope exento de impuestos DGII
}