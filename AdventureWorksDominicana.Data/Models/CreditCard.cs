using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace AdventureWorksDominicana.Data.Models;

/// <summary>
/// Customer credit card information.
/// </summary>
[Table("CreditCard", Schema = "Sales")]
[Index("CardNumber", Name = "AK_CreditCard_CardNumber", IsUnique = true)]
public partial class CreditCard
{
    /// <summary>
    /// Primary key for CreditCard records.
    /// </summary>
    [Key]
    [Column("CreditCardID")]
    public int CreditCardId { get; set; }

    /// <summary>
    /// Credit card name.
    /// </summary>
    [Required(ErrorMessage = "El tipo de tarjeta es obligatorio.")]
    [StringLength(50, ErrorMessage = "El tipo de tarjeta no puede exceder los 50 caracteres.")]
    public string CardType { get; set; } = null!;

    /// <summary>
    /// Credit card number.
    /// </summary>
    [Required(ErrorMessage = "El número de tarjeta es obligatorio.")]
    [Range(16, 16, ErrorMessage = "Número de tarjeta inválida, solo puede registrar 16 números.")]
    [StringLength(25, ErrorMessage = "El número de tarjeta no puede exceder los 25 caracteres.")]
    public string CardNumber { get; set; } = null!;

    /// <summary>
    /// Credit card expiration month.
    /// </summary>
    [Required(ErrorMessage = "El mes de expiración es obligatorio.")]
    [Range(1, 12, ErrorMessage = "El mes de expiración debe estar entre 1 y 12.")]
    public byte ExpMonth { get; set; }

    /// <summary>
    /// Credit card expiration year.
    /// </summary>
    [Required(ErrorMessage = "El año de expiración es obligatorio.")]
    [AnoActualOFuturo]
    public short ExpYear { get; set; }

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    [InverseProperty("CreditCard")]
    public virtual ICollection<PersonCreditCard> PersonCreditCards { get; set; } = new List<PersonCreditCard>();

    [InverseProperty("CreditCard")]
    public virtual ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; } = new List<SalesOrderHeader>();
}
