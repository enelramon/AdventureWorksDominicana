using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorksDominicana.Data.Models;

[Table("Payroll", Schema = "HumanResources")]
public partial class Payroll
{
    [Key]
    public int PayrollId { get; set; }

    [Required(ErrorMessage = "La descripción es necesaria para identificar la nómina.")]
    [StringLength(100, ErrorMessage = "La descripción no puede exceder los 100 caracteres.")]
    [Display(Name = "Descripción")]
    public string Description { get; set; } = null!; // Ejemplo: "1ra Quincena Enero 2024"

    [Required(ErrorMessage = "Debe definir una fecha de inicio.")]
    [Column(TypeName = "date")]
    public DateTime PeriodStartDate { get; set; }

    [Required(ErrorMessage = "Debe definir una fecha de fin.")]
    [Column(TypeName = "date")]
    public DateTime PeriodEndDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime? PaymentDate { get; set; }

    [Required]
    public PayrollStatus Status { get; set; } = PayrollStatus.Borrador;

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [InverseProperty("Payroll")]
    public virtual ICollection<PayrollDetail> PayrollDetails { get; set; } = new List<PayrollDetail>();
}