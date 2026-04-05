using System.Linq.Expressions;
using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Services;

public class VStateProvinceCountryRegionService
{
    private readonly IDbContextFactory<Contexto> _db;

    public VStateProvinceCountryRegionService(IDbContextFactory<Contexto> db)
    {
        _db = db;
    }

    public async Task<List<VStateProvinceCountryRegion>> Listar(Expression<Func<VStateProvinceCountryRegion, bool>> filtro)
    {
        await using var ctx = await _db.CreateDbContextAsync();

        return await ctx.VStateProvinceCountryRegions
            .AsNoTracking()
            .Where(filtro)
            .OrderBy(x => x.CountryRegionName)
            .ThenBy(x => x.StateProvinceName)
            .ToListAsync();
    }
}