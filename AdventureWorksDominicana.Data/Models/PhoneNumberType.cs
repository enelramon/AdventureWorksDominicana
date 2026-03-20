using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Data.Models;

/// <summary>
/// Type of phone number of a person.
/// </summary>
[Table("PhoneNumberType", Schema = "Person")]
public partial class PhoneNumberType
{
    /// <summary>
    /// Primary key for telephone number type records.
    /// </summary>
    [Key]
    [Column("PhoneNumberTypeID")]
    [Required(ErrorMessage = "El Id del tipo de numero de telefono es obligatorio.")]
    public int PhoneNumberTypeId { get; set; }

    /// <summary>
    /// Name of the telephone number type
    /// </summary>
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    [Column(TypeName = "datetime")]
    [Required(ErrorMessage = "La fecha de modificacion es obligatoria.")]
    public DateTime ModifiedDate { get; set; }

    [InverseProperty("PhoneNumberType")]
    public virtual ICollection<PersonPhone> PersonPhones { get; set; } = new List<PersonPhone>();
}
