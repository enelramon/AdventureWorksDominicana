using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdventureWorksDominicana.Data.Models;
//HAGO esto, porque como son validaciones dinámicas, si se ponen datos fijos luego hay que estar actualizando.
public class AnoActualOFuturoAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null && short.TryParse(value.ToString(), out short anioIngresado))
        {
            int anioActual = DateTime.Now.Year;
            int anioMaximo = DateTime.Now.Year + 5;
            if (anioIngresado < anioActual)
            {
                return new ValidationResult(ErrorMessage ?? "Tarjeta vencida.", new[] { validationContext.MemberName! }); //un arreglo indicando el nombre de la propiedad
            }

            if (anioIngresado > anioMaximo)
            {
                return new ValidationResult(ErrorMessage ?? "Año de vencimiento superior a estándares.", new[] { validationContext.MemberName! });
            }
        }

        return ValidationResult.Success;
    }
}