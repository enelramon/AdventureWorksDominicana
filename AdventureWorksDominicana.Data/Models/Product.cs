using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Data.Models;

/// <summary>
/// Products sold or used in the manfacturing of sold products.
/// </summary>
[Table("Product", Schema = "Production")]
[Index("Name", Name = "AK_Product_Name", IsUnique = true)]
[Index("ProductNumber", Name = "AK_Product_ProductNumber", IsUnique = true)]
[Index("Rowguid", Name = "AK_Product_rowguid", IsUnique = true)]
public partial class Product
{
    /// <summary>
    /// Primary key for Product records.
    /// </summary>
    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }

    /// <summary>
    /// Name of the product.
    /// </summary>
    [Required(ErrorMessage = "El nombre es requerido.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres.")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Unique product identification number.
    /// </summary>
    [Required(ErrorMessage = "El número de producto es requerido.")]
    [StringLength(25, MinimumLength = 3, ErrorMessage = "El número de producto debe tener entre 3 y 25 caracteres.")]
    public string ProductNumber { get; set; } = null!;

    /// <summary>
    /// 0 = Product is purchased, 1 = Product is manufactured in-house.
    /// </summary>
    [Required(ErrorMessage = "La bandera Make es requerida.")]
    public bool MakeFlag { get; set; }

    /// <summary>
    /// 0 = Product is not a salable item. 1 = Product is salable.
    /// </summary>
    [Required(ErrorMessage = "La bandera de producto terminado es requerida.")]
    public bool FinishedGoodsFlag { get; set; }

    /// <summary>
    /// Product color.
    /// </summary>
    [StringLength(15, ErrorMessage = "El color debe tener como máximo 15 caracteres.")]
    public string? Color { get; set; }

    /// <summary>
    /// Minimum inventory quantity. 
    /// </summary>
    [Required(ErrorMessage = "El nivel de stock de seguridad es requerido.")]
    [Range(0, short.MaxValue, ErrorMessage = "El nivel de stock de seguridad debe ser mayor o igual a 0.")]
    public short SafetyStockLevel { get; set; }

    /// <summary>
    /// Inventory level that triggers a purchase order or work order. 
    /// </summary>
    [Required(ErrorMessage = "El punto de reorden es requerido.")]
    [Range(0, short.MaxValue, ErrorMessage = "El punto de reorden debe ser mayor o igual a 0.")]
    public short ReorderPoint { get; set; }

    /// <summary>
    /// Standard cost of the product.
    /// </summary>
    [Required(ErrorMessage = "El costo estándar es requerido.")]
    [Range(typeof(decimal), "0", "999999999", ErrorMessage = "El costo estándar debe ser mayor o igual a 0.")]
    [Column(TypeName = "money")]
    public decimal StandardCost { get; set; }

    /// <summary>
    /// Selling price.
    /// </summary>
    [Required(ErrorMessage = "El precio de lista es requerido.")]
    [Range(typeof(decimal), "0", "999999999", ErrorMessage = "El precio de lista debe ser mayor o igual a 0.")]
    [Column(TypeName = "money")]
    public decimal ListPrice { get; set; }

    /// <summary>
    /// Product size.
    /// </summary>
    [StringLength(5, ErrorMessage = "El tamaño debe tener como máximo 5 caracteres.")]
    public string? Size { get; set; }

    /// <summary>
    /// Unit of measure for Size column.
    /// </summary>
    [StringLength(3, ErrorMessage = "La unidad de medida del tamaño debe tener como máximo 3 caracteres.")]
    public string? SizeUnitMeasureCode { get; set; }

    /// <summary>
    /// Unit of measure for Weight column.
    /// </summary>
    [StringLength(3, ErrorMessage = "La unidad de medida del peso debe tener como máximo 3 caracteres.")]
    public string? WeightUnitMeasureCode { get; set; }

    /// <summary>
    /// Product weight.
    /// </summary>
    [Range(typeof(decimal), "0", "99999999", ErrorMessage = "El peso debe ser mayor o igual a 0.")]
    [Column(TypeName = "decimal(8, 2)")]
    public decimal? Weight { get; set; }

    /// <summary>
    /// Number of days required to manufacture the product.
    /// </summary>
    [Required(ErrorMessage = "Los días de fabricación son requeridos.")]
    [Range(0, int.MaxValue, ErrorMessage = "Los días de fabricación deben ser mayor o igual a 0.")]
    public int DaysToManufacture { get; set; }

    /// <summary>
    /// R = Road, M = Mountain, T = Touring, S = Standard
    /// </summary>
    [StringLength(2, ErrorMessage = "La línea de producto debe tener como máximo 2 caracteres.")]
    public string? ProductLine { get; set; }

    /// <summary>
    /// H = High, M = Medium, L = Low
    /// </summary>
    [StringLength(2, ErrorMessage = "La clase debe tener como máximo 2 caracteres.")]
    public string? Class { get; set; }

    /// <summary>
    /// W = Womens, M = Mens, U = Universal
    /// </summary>
    [StringLength(2, ErrorMessage = "El estilo debe tener como máximo 2 caracteres.")]
    public string? Style { get; set; }

    /// <summary>
    /// Product is a member of this product subcategory. Foreign key to ProductSubCategory.ProductSubCategoryID. 
    /// </summary>
    [Column("ProductSubcategoryID")]
    public int? ProductSubcategoryId { get; set; }

    /// <summary>
    /// Product is a member of this product model. Foreign key to ProductModel.ProductModelID.
    /// </summary>
    [Column("ProductModelID")]
    public int? ProductModelId { get; set; }

    /// <summary>
    /// Date the product was available for sale.
    /// </summary>
    [Required(ErrorMessage = "La fecha de inicio de venta es requerida.")]
    [Column(TypeName = "datetime")]
    public DateTime SellStartDate { get; set; }

    /// <summary>
    /// Date the product was no longer available for sale.
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? SellEndDate { get; set; }

    /// <summary>
    /// Date the product was discontinued.
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? DiscontinuedDate { get; set; }

    /// <summary>
    /// ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
    /// </summary>
    [Column("rowguid")]
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    [Required(ErrorMessage = "La fecha de modificación es requerida.")]
    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    [InverseProperty("Component")]
    public virtual ICollection<BillOfMaterial> BillOfMaterialComponents { get; set; } = new List<BillOfMaterial>();

    [InverseProperty("ProductAssembly")]
    public virtual ICollection<BillOfMaterial> BillOfMaterialProductAssemblies { get; set; } = new List<BillOfMaterial>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductCostHistory> ProductCostHistories { get; set; } = new List<ProductCostHistory>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductListPriceHistory> ProductListPriceHistories { get; set; } = new List<ProductListPriceHistory>();

    [ForeignKey("ProductModelId")]
    [InverseProperty("Products")]
    public virtual ProductModel? ProductModel { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<ProductProductPhoto> ProductProductPhotos { get; set; } = new List<ProductProductPhoto>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

    [ForeignKey("ProductSubcategoryId")]
    [InverseProperty("Products")]
    public virtual ProductSubcategory? ProductSubcategory { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<ProductVendor> ProductVendors { get; set; } = new List<ProductVendor>();

    [InverseProperty("Product")]
    public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();

    [InverseProperty("Product")]
    public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

    [ForeignKey("SizeUnitMeasureCode")]
    [InverseProperty("ProductSizeUnitMeasureCodeNavigations")]
    public virtual UnitMeasure? SizeUnitMeasureCodeNavigation { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<SpecialOfferProduct> SpecialOfferProducts { get; set; } = new List<SpecialOfferProduct>();

    [InverseProperty("Product")]
    public virtual ICollection<TransactionHistory> TransactionHistories { get; set; } = new List<TransactionHistory>();

    [ForeignKey("WeightUnitMeasureCode")]
    [InverseProperty("ProductWeightUnitMeasureCodeNavigations")]
    public virtual UnitMeasure? WeightUnitMeasureCodeNavigation { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
}