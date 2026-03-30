using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class UnitMeasureService(IDbContextFactory<Contexto> DbContextFactory) : IService<UnitMeasure, string>
{
    public async Task<UnitMeasure?> Buscar(string id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.UnitMeasures.FirstOrDefaultAsync(u => u.UnitMeasureCode == id);
    }
    public async Task<bool> Eliminar(string id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        var filasAfectadas = await contexto.UnitMeasures.Where(u => u.UnitMeasureCode == id).ExecuteDeleteAsync();
        return filasAfectadas > 0;
    }
    public async Task<List<UnitMeasure>> GetList(Expression<Func<UnitMeasure, bool>> criterio)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.UnitMeasures.Where(criterio).AsNoTracking().ToListAsync();
    }
    public async Task<bool> Guardar(UnitMeasure unitMeasure)
    {
        if (!await Existe(unitMeasure.UnitMeasureCode))
        {
            return await Insertar(unitMeasure);
        }
        else
        {
            return await Modificar(unitMeasure);
        }
    }
    public async Task<bool> Existe(string id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.UnitMeasures.AnyAsync(u => u.UnitMeasureCode == id);
    }
    public async Task<bool> Insertar(UnitMeasure unitMeasure)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        unitMeasure.ModifiedDate = DateTime.Now;
        contexto.UnitMeasures.Add(unitMeasure);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Modificar(UnitMeasure unitMeasure)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        unitMeasure.ModifiedDate = DateTime.Now;
        contexto.Entry(unitMeasure).State = EntityState.Modified;
        return await contexto.SaveChangesAsync() > 0;
    }
}