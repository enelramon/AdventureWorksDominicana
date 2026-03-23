using System.Linq.Expressions;
using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Services;

public class CurrencyService(IDbContextFactory<Contexto> DbFactory) : IService<Currency, string>
{
    public async Task<bool> Guardar(Currency entidad)
    {
        entidad.ModifiedDate = DateTime.Now; 

        if (!await Existe(entidad.CurrencyCode))
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
        return await contexto.Currencies.AnyAsync(e => e.CurrencyCode == id);
    }

    private async Task<bool> Insertar(Currency entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Currencies.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Currency entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Currencies.Update(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Currency?> Buscar(string id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Currencies.FirstOrDefaultAsync(a => a.CurrencyCode == id);
    }

    public async Task<bool> Eliminar(string id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Currencies.Where(a => a.CurrencyCode == id).ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Currency>> GetList(Expression<Func<Currency, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Currencies.AsNoTracking().Where(criterio).ToListAsync();
    }
}