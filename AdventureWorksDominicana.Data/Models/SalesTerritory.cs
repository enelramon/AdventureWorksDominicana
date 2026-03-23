using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Data.Models;

/// <summary>
/// Sales territory lookup table.
/// </summary>
[Table("SalesTerritory", Schema = "Sales")]
[Index("Name", Name = "AK_SalesTerritory_Name", IsUnique = true)]
[Index("Rowguid", Name = "AK_SalesTerritory_rowguid", IsUnique = true)]
public partial class SalesTerritory
{
    /// <summary>
    /// Primary key for SalesTerritory records.
    /// </summary>
    [Key]
    [Column("TerritoryID")]
    [Required(ErrorMessage = "El Id del territorio de ventas es requerido.")]
    public int TerritoryId { get; set; }

    /// <summary>
    /// Sales territory description
    /// </summary>
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres.")]
    [Required(ErrorMessage = "El nombre del territorio de ventas es requerido.")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// ISO standard country or region code. Foreign key to CountryRegion.CountryRegionCode. 
    /// </summary>
    [StringLength(3, ErrorMessage = "El codigo del pais/region debe tener como maximo 3 caracteres.")]
    [Required(ErrorMessage = "El codigo del pais/region es requerido.")]
    public string CountryRegionCode { get; set; } = null!;

    /// <summary>
    /// Geographic area to which the sales territory belong.
    /// </summary>
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El grupo debe tener entre 3 y 50 caracteres.")]
    [Required(ErrorMessage = "El grupo del territorio de ventas es requerido.")]
    public string Group { get; set; } = null!;

    /// <summary>
    /// Sales in the territory year to date.
    /// </summary>
    [Column("SalesYTD", TypeName = "money")]
    [Range(0, double.MaxValue, ErrorMessage = "Las ventas del presente anio no pueden ser negativas.")]
    public decimal SalesYtd { get; set; }

    /// <summary>
    /// Sales in the territory the previous year.
    /// </summary>
    [Column(TypeName = "money")]
    [Range(0, double.MaxValue, ErrorMessage = "Las ventas del pasado anio no pueden ser negativas.")]
    public decimal SalesLastYear { get; set; }

    /// <summary>
    /// Business costs in the territory year to date.
    /// </summary>
    [Column("CostYTD", TypeName = "money")]
    [Range(0, double.MaxValue, ErrorMessage = "Los costos del presente anio no pueden ser negativas.")]
    public decimal CostYtd { get; set; }

    /// <summary>
    /// Business costs in the territory the previous year.
    /// </summary>
    [Column(TypeName = "money")]
    [Range(0, double.MaxValue, ErrorMessage = "Los costos del pasado anio no pueden ser negativas.")]
    public decimal CostLastYear { get; set; }

    /// <summary>
    /// ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
    /// </summary>
    [Column("rowguid")]
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    [Column(TypeName = "datetime")]
    [Required(ErrorMessage = "La fecha de modificacion es requerida.")]
    public DateTime ModifiedDate { get; set; }

    [ForeignKey("CountryRegionCode")]
    [InverseProperty("SalesTerritories")]
    public virtual CountryRegion CountryRegionCodeNavigation { get; set; } = null!;

    [InverseProperty("Territory")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    [InverseProperty("Territory")]
    public virtual ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; } = new List<SalesOrderHeader>();

    [InverseProperty("Territory")]
    public virtual ICollection<SalesPerson> SalesPeople { get; set; } = new List<SalesPerson>();

    [InverseProperty("Territory")]
    public virtual ICollection<SalesTerritoryHistory> SalesTerritoryHistories { get; set; } = new List<SalesTerritoryHistory>();

    [InverseProperty("Territory")]
    public virtual ICollection<StateProvince> StateProvinces { get; set; } = new List<StateProvince>();
}
