using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Services;

internal class Currencyservice(IDbContextFactory<Contexto> DbFactory) : IService<Currency, string>
{
    public async Task<bool> Guardar(Currency entidad)
    {
        if (!await Existe(entidad))
        {
            return await Insertar(entidad);
        }
        else
        {
            return await Modificar(entidad);
        }
    }
    public async Task<bool> Existe(Currency entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Currencies.AnyAsync(e => e.CurrencyCode == entidad.CurrencyCode);
    }
    public async Task<bool> Insertar(Currency entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Currencies.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Modificar(Currency entidad)
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
        var currency = await Buscar(id);

        if (currency == null)
            return false;

        contexto.Currencies.Remove(currency);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Currency>> GetList(Expression<Func<Currency, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Currencies.AsNoTracking().Where(criterio).ToListAsync();
    }
}
