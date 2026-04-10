using System.Linq.Expressions;
using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Services;

public class LocationService(IDbContextFactory<Contexto> DbFactory) : IService<Location, short>
{
    public async Task<bool> Existe(short id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Locations.AnyAsync(l => l.LocationId == id);
    }

    public async Task<bool> Guardar(Location location)
    {
        if (!await Existe(location.LocationId))
            return await Insertar(location);

        return await Modificar(location);
    }

    private async Task<bool> Insertar(Location location)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        location.ModifiedDate = DateTime.Now;
        contexto.Locations.Add(location);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Location location)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        location.ModifiedDate = DateTime.Now;
        contexto.Locations.Update(location);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Location?> Buscar(short id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Locations.AsNoTracking()
            .FirstOrDefaultAsync(l => l.LocationId == id);
    }

    public async Task<bool> Eliminar(short id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        try
        {
            return await contexto.Locations.Where(l => l.LocationId == id).ExecuteDeleteAsync() > 0;
        }
        catch { return false; }
    }

    public async Task<List<Location>> GetList(Expression<Func<Location, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Locations.AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}