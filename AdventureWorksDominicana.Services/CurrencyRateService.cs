using System.Linq.Expressions;
using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Services;

public class CurrencyRateService(IDbContextFactory<Contexto> DbFactory) : IService<CurrencyRate, int>
{
    public async Task<bool> Guardar(CurrencyRate entidad)
    {
        entidad.ModifiedDate = DateTime.Now; 
        if (!await Existe(entidad.CurrencyRateId))
        {
            return await Insertar(entidad);
        }
        else
        {
            return await Modificar(entidad);
        }
    }

    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.CurrencyRates.AnyAsync(e => e.CurrencyRateId == id);
    }

    public async Task<bool> Insertar(CurrencyRate entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.CurrencyRates.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(CurrencyRate entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.CurrencyRates.Update(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<CurrencyRate?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.CurrencyRates.FirstOrDefaultAsync(a => a.CurrencyRateId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.CurrencyRates.Where(a => a.CurrencyRateId == id).ExecuteDeleteAsync() > 0;
    }

    public async Task<List<CurrencyRate>> GetList(Expression<Func<CurrencyRate, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.CurrencyRates.AsNoTracking().Where(criterio).ToListAsync();
    }
}