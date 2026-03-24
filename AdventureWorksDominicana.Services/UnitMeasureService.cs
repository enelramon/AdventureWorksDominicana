using System.Linq.Expressions;
using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Services;

public class UnitMeasureService(IDbContextFactory<Contexto> DbFactory) : IService<UnitMeasure, string>
{
    public async Task<bool> Guardar(UnitMeasure entidad)
    {
        entidad.ModifiedDate = DateTime.Now; 

        if (!await Existe(entidad.UnitMeasureCode))
        {
            return await Insertar(entidad);
        }
        else
        {
            return await Modificar(entidad);
        }
    }

    public async Task<bool> Existe(string id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.UnitMeasures.AnyAsync(e => e.UnitMeasureCode == id);
    }

    private async Task<bool> Insertar(UnitMeasure entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.UnitMeasures.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(UnitMeasure entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.UnitMeasures.Update(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<UnitMeasure?> Buscar(string id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.UnitMeasures.FirstOrDefaultAsync(a => a.UnitMeasureCode == id);
    }

    public async Task<bool> Eliminar(string id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.UnitMeasures.Where(a => a.UnitMeasureCode == id).ExecuteDeleteAsync() > 0;
    }

    public async Task<List<UnitMeasure>> GetList(Expression<Func<UnitMeasure, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.UnitMeasures.AsNoTracking().Where(criterio).ToListAsync();
    }
}