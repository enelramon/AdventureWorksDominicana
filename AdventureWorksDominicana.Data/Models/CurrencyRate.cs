using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Data.Models;

/// <summary>
/// Currency exchange rates.
/// </summary>
[Table("CurrencyRate", Schema = "Sales")]
[Index("CurrencyRateDate", "FromCurrencyCode", "ToCurrencyCode", Name = "AK_CurrencyRate_CurrencyRateDate_FromCurrencyCode_ToCurrencyCode", IsUnique = true)]
public partial class CurrencyRate
{
    /// <summary>
    /// Primary key for CurrencyRate records.
    /// </summary>
    [Key]
    [Column("CurrencyRateID")]
    public int CurrencyRateId { get; set; }

    /// <summary>
    /// Date and time the exchange rate was obtained.
    /// </summary>
    [Column(TypeName = "datetime")]
    [Required(ErrorMessage = "La fecha es requerida.")]
    public DateTime CurrencyRateDate { get; set; }

    /// <summary>
    /// Exchange rate was converted from this currency code.
    /// </summary>
    [StringLength(3)]
    [Required(ErrorMessage = "El código de origen es requerido.")]
    public string FromCurrencyCode { get; set; } = null!;

    /// <summary>
    /// Exchange rate was converted to this currency code.
    /// </summary>
    [StringLength(3)]
    [Required(ErrorMessage = "El código de destino es requerido.")]
    public string ToCurrencyCode { get; set; } = null!;

    /// <summary>
    /// Average exchange rate for the day.
    /// </summary>
    [Column(TypeName = "money")]
    [Required(ErrorMessage = "La tasa promedio es requerida.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "La tasa debe ser un número mayor a 0.")]
    public decimal AverageRate { get; set; }

    /// <summary>
    /// Final exchange rate for the day.
    /// </summary>
    [Column(TypeName = "money")]
    [Required(ErrorMessage = "La tasa de cierre es requerida.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "La tasa debe ser un número mayor a 0.")]
    public decimal EndOfDayRate { get; set; }

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    [ForeignKey("FromCurrencyCode")]
    [InverseProperty("CurrencyRateFromCurrencyCodeNavigations")]
    public virtual Currency FromCurrencyCodeNavigation { get; set; } = null!;

    [InverseProperty("CurrencyRate")]
    public virtual ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; } = new List<SalesOrderHeader>();

    [ForeignKey("ToCurrencyCode")]
    [InverseProperty("CurrencyRateToCurrencyCodeNavigations")]
    public virtual Currency ToCurrencyCodeNavigation { get; set; } = null!;
}
