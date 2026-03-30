using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class EmployeeService(IDbContextFactory<Contexto> DbFactory) : IService<Employee, int>
{
    public async Task<bool> Guardar(Employee entidad)
    {
        if (!await Existe(entidad.BusinessEntityId))
        {
            return await Insertar(entidad);
        }
        else
        {
            return await Modificar(entidad);
        }
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Employees.AnyAsync(e => e.BusinessEntityId == id);
    }

    private async Task<bool> Insertar(Employee entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Employees.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Employee entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Employees.Update(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Employee?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Employees.Include(e => e.BusinessEntity).FirstOrDefaultAsync(e => e.BusinessEntityId == id);
    }

    public async Task<Employee?> Buscar(int id, int idExcluido = 0)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Employees.Include(e => e.BusinessEntity).FirstOrDefaultAsync(e => e.BusinessEntityId == id && e.BusinessEntityId != idExcluido);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Employees.Where(e => e.BusinessEntityId == id).ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Employee>> GetList(Expression<Func<Employee, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Employees.Include(e => e.BusinessEntity).Where(criterio).ToListAsync();
    }
}