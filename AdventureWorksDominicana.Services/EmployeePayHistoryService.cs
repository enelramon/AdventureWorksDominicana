using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class EmployeePayHistoryService(IDbContextFactory<Contexto> DbFactory) : IService<EmployeePayHistory, int>
{
    public async Task<bool> Guardar(EmployeePayHistory entidad)
    {
        if (!await Existe(entidad.BusinessEntityId, entidad.RateChangeDate))
        {
            return await Insertar(entidad);
        }
        else
        {
            return await Modificar(entidad);
        }
    }

    private async Task<bool> Existe(int id, DateTime fechaCambio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EmployeePayHistories
            .AnyAsync(e => e.BusinessEntityId == id && e.RateChangeDate == fechaCambio);
    }

    private async Task<bool> Insertar(EmployeePayHistory entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        entidad.ModifiedDate = DateTime.Now;
        contexto.EmployeePayHistories.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(EmployeePayHistory entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        entidad.ModifiedDate = DateTime.Now;
        contexto.EmployeePayHistories.Update(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    // (Busca por el ID de empleado, trae el historial completo)
    public async Task<EmployeePayHistory?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EmployeePayHistories
            .Where(e => e.BusinessEntityId == id)
            .OrderByDescending(e => e.RateChangeDate)
            .FirstOrDefaultAsync();
    }

    // Sobrecarga para buscar un registro específico de la llave compuesta
    public async Task<EmployeePayHistory?> BuscarEspecifico(int id, DateTime fecha)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EmployeePayHistories
            .FirstOrDefaultAsync(e => e.BusinessEntityId == id && e.RateChangeDate == fecha);
    }

    public async Task<bool> Eliminar(int id)
    {
        // En llaves compuestas, eliminar solo por ID de empleado podría borrar TODO su historial.
        // Por eso uso la sobrecarga con fecha para seguridad
        throw new NotImplementedException();
    }

    public async Task<bool> Eliminar(int id, DateTime fecha)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EmployeePayHistories
            .Where(e => e.BusinessEntityId == id && e.RateChangeDate == fecha)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<EmployeePayHistory>> GetList(Expression<Func<EmployeePayHistory, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EmployeePayHistories
            .Where(criterio)
            .ToListAsync();
    }
}