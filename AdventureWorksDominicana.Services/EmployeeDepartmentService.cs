using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class EmployeeDepartmentService(IDbContextFactory<Contexto> DbFactory) : IService<EmployeeDepartmentHistory, int>
{
    // Método especializado para Transferencias
    public async Task<bool> TransferirEmpleado(int employeeId, short newDepartmentId, byte newShiftId, DateOnly startDate)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        await using var transaction = await contexto.Database.BeginTransactionAsync();

        try
        {
            // Buscar la asignación actual (la que no tiene fecha de fin)
            var asignacionActual = await contexto.EmployeeDepartmentHistories
                .FirstOrDefaultAsync(h => h.BusinessEntityId == employeeId && h.EndDate == null);

            if (asignacionActual != null)
            {
                // Validar que no lo estén mandando al mismo exacto lugar y tanda
                if (asignacionActual.DepartmentId == newDepartmentId && asignacionActual.ShiftId == newShiftId)
                {
                    return false; // Ya está en ese depto y tanda
                }

                // Cerramos el ciclo anterior (un día antes de que empiece el nuevo)
                asignacionActual.EndDate = startDate.AddDays(-1);
                asignacionActual.ModifiedDate = DateTime.Now;
                contexto.EmployeeDepartmentHistories.Update(asignacionActual);
            }

            // 2. Crear la nueva asignación
            var nuevaAsignacion = new EmployeeDepartmentHistory
            {
                BusinessEntityId = employeeId,
                DepartmentId = newDepartmentId,
                ShiftId = newShiftId,
                StartDate = startDate,
                EndDate = null, // Se queda abierto
                ModifiedDate = DateTime.Now
            };

            contexto.EmployeeDepartmentHistories.Add(nuevaAsignacion);

            await contexto.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return false;
        }
    }

    public async Task<List<EmployeeDepartmentHistory>> GetList(Expression<Func<EmployeeDepartmentHistory, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EmployeeDepartmentHistories
            .Include(h => h.Department)
            .Include(h => h.Shift)
            .Where(criterio)
            .OrderByDescending(h => h.StartDate)
            .ToListAsync();
    }

    public Task<bool> Guardar(EmployeeDepartmentHistory entidad) => throw new NotImplementedException();
    public Task<EmployeeDepartmentHistory?> Buscar(int id) => throw new NotImplementedException();
    public Task<bool> Eliminar(int id) => throw new NotImplementedException();
}