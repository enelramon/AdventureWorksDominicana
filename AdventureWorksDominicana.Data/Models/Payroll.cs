using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorksDominicana.Data.Models;

[Table("Payroll", Schema = "HumanResources")]
public partial class Payroll
{
    [Key]
    public int PayrollId { get; set; }

    [Required(ErrorMessage = "La descripción de la nómina es obligatoria.")]
    [StringLength(100)]
    public string Description { get; set; } = null!; // Ejemplo: "1ra Quincena Enero 2024"

    [Required]
    [Column(TypeName = "date")]
    public DateTime PeriodStartDate { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime PeriodEndDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime? PaymentDate { get; set; }

    [Required]
    [StringLength(20)]
    public PayrollStatus Status { get; set; } = PayrollStatus.Borrador;

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [InverseProperty("Payroll")]
    public virtual ICollection<PayrollDetail> PayrollDetails { get; set; } = new List<PayrollDetail>();
}