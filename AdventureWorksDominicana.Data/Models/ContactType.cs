using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Data.Models;

/// <summary>
/// Lookup table containing the types of business entity contacts.
/// </summary>
[Table("ContactType", Schema = "Person")]
[Index("Name", Name = "AK_ContactType_Name", IsUnique = true)]
public partial class ContactType
{
    /// <summary>
    /// Primary key for ContactType records.
    /// </summary>
    [Key]
    [Column("ContactTypeID")]
    [Required(ErrorMessage = "El Id es requerido.")]
    public int ContactTypeId { get; set; }

    /// <summary>
    /// Contact type description.
    /// </summary>
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(50, ErrorMessage = "El nombre debe contener entre 3 y 50 caracteres.")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    [Column(TypeName = "datetime")]
    [Required(ErrorMessage = "La fecha de modificacion es obligatoria.")]
    public DateTime ModifiedDate { get; set; }

    [InverseProperty("ContactType")]
    public virtual ICollection<BusinessEntityContact> BusinessEntityContacts { get; set; } = new List<BusinessEntityContact>();
}
