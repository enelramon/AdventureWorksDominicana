using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Data.Models;

/// <summary>
/// Record of each purchase order, sales order, or work order transaction year to date.
/// </summary>
[Table("TransactionHistory", Schema = "Production")]
[Index("ProductId", Name = "IX_TransactionHistory_ProductID")]
[Index("ReferenceOrderId", "ReferenceOrderLineId", Name = "IX_TransactionHistory_ReferenceOrderID_ReferenceOrderLineID")]
public partial class TransactionHistory
{
    /// <summary>
    /// Primary key for TransactionHistory records.
    /// </summary>
    [Key]
    [Column("TransactionID")]
    public int TransactionId { get; set; }

    /// <summary>
    /// Product identification number. Foreign key to Product.ProductID.
    /// </summary>
    [Required(ErrorMessage = "Campo requerido.")]
    [Range(1, int.MaxValue, ErrorMessage = "Campo requerido.")]
    [Column("ProductID")]
    public int ProductId { get; set; }

    /// <summary>
    /// Purchase order, sales order, or work order identification number.
    /// </summary>
    [Required(ErrorMessage = "Campo requerido.")]
    [Range(1, int.MaxValue, ErrorMessage = "Campo requerido.")]
    [Column("ReferenceOrderID")]
    public int ReferenceOrderId { get; set; }

    /// <summary>
    /// Line number associated with the purchase order, sales order, or work order.
    /// </summary>
    [Required(ErrorMessage = "Campo requerido.")]
    [Range(1, int.MaxValue, ErrorMessage = "Campo requerido.")]
    [Column("ReferenceOrderLineID")]
    public int ReferenceOrderLineId { get; set; }

    /// <summary>
    /// Date and time of the transaction.
    /// </summary>
    [Required(ErrorMessage = "Campo requerido.")]
    [Column(TypeName = "datetime")]
    public DateTime TransactionDate { get; set; }

    /// <summary>
    /// W = WorkOrder, S = SalesOrder, P = PurchaseOrder
    /// </summary>
    [Required(ErrorMessage = "Campo requerido.")]
    [StringLength(1)]
    public string TransactionType { get; set; } = null!;

    /// <summary>
    /// Product quantity.
    /// </summary>
    [Required(ErrorMessage = "Campo requerido.")]
    [Range(1, int.MaxValue, ErrorMessage = "El valor debe ser mayor que 0.")]
    public int Quantity { get; set; }

    /// <summary>
    /// Product cost.
    /// </summary>
    [Required(ErrorMessage = "Campo requerido.")]
    [Range(0.0001, double.MaxValue, ErrorMessage = "El valor debe ser mayor que 0.")]
    [Column(TypeName = "money")]
    public decimal ActualCost { get; set; }

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    [Required(ErrorMessage = "Campo requerido.")]
    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("TransactionHistories")]
    public virtual Product Product { get; set; } = null!;
}