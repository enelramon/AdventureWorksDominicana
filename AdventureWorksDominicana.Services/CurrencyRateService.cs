using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Microsoft.EntityFrameworkCore;
using Aplicada1.Core;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class CurrencyRateService(IDbContextFactory<Contexto> DbFactory) : IService<CurrencyRate, int>
{
    public async Task<CurrencyRate?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.CurrencyRates
            .FirstOrDefaultAsync(c => c.CurrencyRateId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.CurrencyRates
            .Where(c => c.CurrencyRateId == id)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<CurrencyRate>> GetList(Expression<Func<CurrencyRate, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.CurrencyRates
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> Guardar(CurrencyRate entidad)
    {
        if (!await Existe(entidad.CurrencyRateId))
        {
            return await Insertar(entidad);
        }
        else
        {
            return await Modificar(entidad);
        }
    }

    public async Task<bool> Insertar(CurrencyRate entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.CurrencyRates.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int idEntidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.CurrencyRates.AnyAsync(c => c.CurrencyRateId == idEntidad);
    }

    public async Task<bool> Modificar(CurrencyRate entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }
}