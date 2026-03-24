using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Data.Models;

/// <summary>
/// Lookup table containing the ISO standard codes for countries and regions.
/// </summary>
[Table("CountryRegion", Schema = "Person")]
[Index("Name", Name = "AK_CountryRegion_Name", IsUnique = true)]
public partial class CountryRegion
{
    /// <summary>
    /// ISO standard code for countries and regions.
    /// </summary>
    [Key]
    [StringLength(3, ErrorMessage = "El codigo del pais/region debe tener de 1 a 3 caracteres.")]
    [Required(ErrorMessage = "El codigo del pais/region es requerido.")]
    public string CountryRegionCode { get; set; } = null!;

    /// <summary>
    /// Country or region name.
    /// </summary>
    [Required(ErrorMessage = "El nombre del pais/region es requerido.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre del pais/region debe tener entre 3 y 50 caracteres.")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    [Required(ErrorMessage = "La fecha de modificacion del pais/region es requerido.")]
    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    [InverseProperty("CountryRegionCodeNavigation")]
    public virtual ICollection<CountryRegionCurrency> CountryRegionCurrencies { get; set; } = new List<CountryRegionCurrency>();

    [InverseProperty("CountryRegionCodeNavigation")]
    public virtual ICollection<SalesTerritory> SalesTerritories { get; set; } = new List<SalesTerritory>();

    [InverseProperty("CountryRegionCodeNavigation")]
    public virtual ICollection<StateProvince> StateProvinces { get; set; } = new List<StateProvince>();
}
