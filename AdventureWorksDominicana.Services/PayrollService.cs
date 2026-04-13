using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text;

namespace AdventureWorksDominicana.Services;

public class PayrollService(IDbContextFactory<Contexto> DbFactory) : IService<Payroll, int>
{
    private const decimal TASA_CAMBIO = 60.50m; // Constante de conversión

    public async Task<bool> Guardar(Payroll entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        foreach (var detalle in entidad.PayrollDetails)
        {
            detalle.Employee = null;
        }

        if (entidad.PayrollId == 0)
        {
            entidad.CreatedDate = DateTime.Now;
            contexto.Payrolls.Add(entidad);
        }
        else
        {
            contexto.Payrolls.Update(entidad);
        }

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Payroll?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Payrolls
            .Include(p => p.PayrollDetails)
                .ThenInclude(d => d.Employee)
                    .ThenInclude(e => e.BusinessEntity)
            .FirstOrDefaultAsync(p => p.PayrollId == id);
    }

    public async Task<List<Payroll>> GetList(Expression<Func<Payroll, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Payrolls.Where(criterio).ToListAsync();
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Payrolls.Where(p => p.PayrollId == id).ExecuteDeleteAsync() > 0;
    }

    public async Task<List<string>> ProcesarNomina(Payroll nominaBorrador)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        List<string> errores = new();

        var parametros = await contexto.PayrollParameters.AsNoTracking().FirstOrDefaultAsync(p => p.IsActive);
        if (parametros == null) throw new Exception("No hay parámetros de nómina activos.");

        var empleados = await contexto.Employees
            .AsNoTracking()
            .Include(e => e.BusinessEntity)
            .Where(e => e.CurrentFlag)
            .ToListAsync();

        decimal topeSFS = parametros.MinimumWage * 10;
        decimal topeAFP = parametros.MinimumWage * 20;

        int diasLaborables = CalcularDiasLaborables(nominaBorrador.PeriodStartDate, nominaBorrador.PeriodEndDate);

        foreach (var emp in empleados)
        {
            var sueldoActual = await contexto.EmployeePayHistories
                .AsNoTracking()
                .Where(h => h.BusinessEntityId == emp.BusinessEntityId)
                .OrderByDescending(h => h.RateChangeDate)
                .FirstOrDefaultAsync();

            string nombreEmpleado = emp.BusinessEntity != null
                    ? $"{emp.BusinessEntity.FirstName} {emp.BusinessEntity.LastName}"
                    : $"ID: {emp.BusinessEntityId}";

            if (sueldoActual == null || sueldoActual.Rate <= 0)
            {
                errores.Add($"Advertencia: {nombreEmpleado} fue omitido porque no tiene un sueldo registrado.");
                continue;
            }

            DateOnly inicioPeriodo = DateOnly.FromDateTime(nominaBorrador.PeriodStartDate);
            DateOnly finPeriodo = DateOnly.FromDateTime(nominaBorrador.PeriodEndDate);

            var asignacionDepto = await contexto.EmployeeDepartmentHistories
                .AsNoTracking()
                .Include(ed => ed.Shift)
                .Where(ed => ed.BusinessEntityId == emp.BusinessEntityId &&
                             ed.StartDate <= finPeriodo &&
                             (ed.EndDate == null || ed.EndDate >= inicioPeriodo))
                .OrderByDescending(ed => ed.StartDate)
                .FirstOrDefaultAsync();

            if (asignacionDepto?.Shift == null)
            {
                errores.Add($"Error: El empleado {nombreEmpleado} no tiene una tanda (Shift) asignada.");
                continue;
            }

            TimeSpan jornada = asignacionDepto.Shift.EndTime - asignacionDepto.Shift.StartTime;
            decimal horasDiarias = (decimal)jornada.TotalHours;
            if (horasDiarias > 5) horasDiarias -= 1;

            // CONVERSIÓN DE USD (BD) A DOP (RD$) PARA LOS CÁLCULOS
            decimal sueldoPorHoraRD = sueldoActual.Rate * TASA_CAMBIO;

            decimal totalHorasTrabajadas = horasDiarias * diasLaborables;
            decimal sueldoBrutoPeriodo = sueldoPorHoraRD * totalHorasTrabajadas;
            decimal sueldoMensualProyectado = (sueldoPorHoraRD * horasDiarias) * 23.83m;

            decimal baseSFS = Math.Min(sueldoMensualProyectado, topeSFS);
            decimal baseAFP = Math.Min(sueldoMensualProyectado, topeAFP);

            decimal descuentoSFSMensual = baseSFS * parametros.SfsPct;
            decimal descuentoAFPMensual = baseAFP * parametros.AfpPct;

            decimal sueldoNetoAntesISR = sueldoMensualProyectado - descuentoSFSMensual - descuentoAFPMensual;
            decimal isrMensual = CalcularISR(sueldoNetoAntesISR, parametros.IsrAnnualExemption);

            decimal proporcion = (decimal)diasLaborables / 23.83m;

            decimal dSFS = descuentoSFSMensual * proporcion;
            decimal dAFP = descuentoAFPMensual * proporcion;
            decimal dISR = isrMensual * proporcion;

            nominaBorrador.PayrollDetails.Add(new PayrollDetail
            {
                BusinessEntityId = emp.BusinessEntityId,
                GrossSalary = sueldoBrutoPeriodo,
                SfsDeduction = dSFS,
                AfpDeduction = dAFP,
                IsrDeduction = dISR,
                NetSalary = sueldoBrutoPeriodo - dSFS - dAFP - dISR
            });
        }
        return errores;
    }

    private int CalcularDiasLaborables(DateTime fechaInicio, DateTime fechaFin)
    {
        int dias = 0;
        DateTime fechaActual = fechaInicio;
        while (fechaActual <= fechaFin)
        {
            if (fechaActual.DayOfWeek != DayOfWeek.Saturday && fechaActual.DayOfWeek != DayOfWeek.Sunday) dias++;
            fechaActual = fechaActual.AddDays(1);
        }
        return dias > 0 ? dias : 1;
    }

    private decimal CalcularISR(decimal sueldoMensualNeto, decimal exencionAnual)
    {
        decimal sueldoAnual = sueldoMensualNeto * 12;
        if (sueldoAnual <= exencionAnual) return 0;

        decimal isrAnual = 0;
        decimal escala2 = 624329.00m;
        decimal escala3 = 867123.00m;

        if (sueldoAnual <= escala2) isrAnual = (sueldoAnual - exencionAnual) * 0.15m;
        else if (sueldoAnual <= escala3) isrAnual = 31216.00m + ((sueldoAnual - escala2) * 0.20m);
        else isrAnual = 79776.00m + ((sueldoAnual - escala3) * 0.25m);

        return isrAnual / 12;
    }

    public async Task<string> GenerarArchivoTxtBanreservas(int payrollId)
    {
        var nomina = await Buscar(payrollId);
        if (nomina == null || !nomina.PayrollDetails.Any()) return string.Empty;

        var sb = new StringBuilder();

        //  ENCABEZADO (41 caracteres) 
        string rncEmpresa = "101000001".PadRight(11, ' ');
        string fechaProceso = DateTime.Now.ToString("yyyyMMdd");
        string totalEmpleados = nomina.PayrollDetails.Count.ToString().PadLeft(6, '0');

        decimal montoTotal = nomina.PayrollDetails.Sum(d => d.NetSalary);
        // Convertimos a centavos (multiplicar por 100) y rellenamos con ceros
        string montoTotalStr = ((long)Math.Round(montoTotal * 100)).ToString().PadLeft(15, '0');

        sb.AppendLine($"E{rncEmpresa}{fechaProceso}{totalEmpleados}{montoTotalStr}");

        // DETALLE (72 caracteres)
        foreach (var det in nomina.PayrollDetails)
        {
            //  Cédula (11)
            string cedula = det.Employee?.NationalIdnumber?.Replace("-", "").Replace(" ", "") ?? "";
            cedula = cedula.PadRight(11, ' ');

            // Cuenta Bancaria (15) - Usamos el campo que agregé a Employee 
            string cuenta = det.Employee?.BankAccountNumber?.Trim() ?? "0000000000";
            cuenta = cuenta.PadLeft(15, '0');

            //  Monto Neto (15) - Centavos sin punto decimal
            string montoNetoStr = ((long)Math.Round(det.NetSalary * 100)).ToString().PadLeft(15, '0');

            //  Nombre (30) - Limpio de acentos y en Mayúsculas
            string nombre = QuitarAcentos($"{det.Employee?.BusinessEntity?.FirstName} {det.Employee?.BusinessEntity?.LastName}".ToUpper());
            nombre = nombre.Length > 30 ? nombre.Substring(0, 30) : nombre.PadRight(30, ' ');

            sb.AppendLine($"D{cedula}{cuenta}{montoNetoStr}{nombre}");
        }

        return sb.ToString();
    }

    private string QuitarAcentos(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto)) return string.Empty;

        // Normaliza el texto para separar las tildes de las letras
        var normalizedString = texto.Normalize(System.Text.NormalizationForm.FormD);
        var stringBuilder = new System.Text.StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
            // Si el carácter no es una tilde/acento, lo agregamos
            if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        // Retorna el texto limpio, normalizado de vuelta y reemplaza la Ñ manualmente 
        // ya que algunos bancos viejos no la procesan correctamente.
        return stringBuilder.ToString()
            .Normalize(System.Text.NormalizationForm.FormC)
            .Replace("Ñ", "N")
            .Replace("ñ", "n");
    }
}