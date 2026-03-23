using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Services;

public class ScrapReasonService
{
    private readonly IDbContextFactory<Contexto> _db;

    public ScrapReasonService(IDbContextFactory<Contexto> db)
    {
        _db = db;
    }

    public async Task<List<ScrapReason>> Listar()
    {
        await using var ctx = await _db.CreateDbContextAsync();
        return await ctx.ScrapReasons
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<ScrapReason?> Buscar(int id)
    {
        await using var ctx = await _db.CreateDbContextAsync();
        return await ctx.ScrapReasons.FindAsync(id);
    }

    public async Task<bool> Guardar(ScrapReason entity)
    {
        await using var ctx = await _db.CreateDbContextAsync();

        if (entity.ScrapReasonId == 0)
            ctx.ScrapReasons.Add(entity);
        else
            ctx.ScrapReasons.Update(entity);

        return await ctx.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var ctx = await _db.CreateDbContextAsync();

        var entity = await ctx.ScrapReasons.FindAsync(id);
        if (entity == null) return false;

        ctx.ScrapReasons.Remove(entity);
        return await ctx.SaveChangesAsync() > 0;
    }
}