using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Data.Models;

/// <summary>
/// State and province lookup table.
/// </summary>
[Table("StateProvince", Schema = "Person")]
[Index("Name", Name = "AK_StateProvince_Name", IsUnique = true)]
[Index("StateProvinceCode", "CountryRegionCode", Name = "AK_StateProvince_StateProvinceCode_CountryRegionCode", IsUnique = true)]
[Index("Rowguid", Name = "AK_StateProvince_rowguid", IsUnique = true)]
public partial class StateProvince
{
    /// <summary>
    /// Primary key for StateProvince records.
    /// </summary>
    [Key]
    [Column("StateProvinceID")]
    public int StateProvinceId { get; set; }

    /// <summary>
    /// ISO standard state or province code.
    /// </summary>
    [StringLength(3, ErrorMessage = "El codigo del estado/provincia debe tener como maximo 3 caracteres.")]
    [Required(ErrorMessage = "El codigo del estado/provincia es requerido.")]
    public string StateProvinceCode { get; set; } = null!;

    /// <summary>
    /// ISO standard country or region code. Foreign key to CountryRegion.CountryRegionCode. 
    /// </summary>
    [StringLength(3, ErrorMessage = "El codigo del pais/region debe tener como maximo 3 caracteres.")]
    [Required(ErrorMessage = "El codigo del pais/region es requerido.")]
    public string CountryRegionCode { get; set; } = null!;

    /// <summary>
    /// 0 = StateProvinceCode exists. 1 = StateProvinceCode unavailable, using CountryRegionCode.
    /// </summary>
    [Required(ErrorMessage = "La bandera de estado/provincia unico es requerido.")]
    public bool IsOnlyStateProvinceFlag { get; set; }

    /// <summary>
    /// State or province description.
    /// </summary>
    [Required(ErrorMessage = "El nombre es requerido.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres.")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// ID of the territory in which the state or province is located. Foreign key to SalesTerritory.SalesTerritoryID.
    /// </summary>
    [Column("TerritoryID")]
    public int TerritoryId { get; set; }

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

    [InverseProperty("StateProvince")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    [ForeignKey("CountryRegionCode")]
    [InverseProperty("StateProvinces")]
    public virtual CountryRegion CountryRegionCodeNavigation { get; set; } = null!;

    [InverseProperty("StateProvince")]
    public virtual ICollection<SalesTaxRate> SalesTaxRates { get; set; } = new List<SalesTaxRate>();

    [ForeignKey("TerritoryId")]
    [InverseProperty("StateProvinces")]
    public virtual SalesTerritory Territory { get; set; } = null!;
}
