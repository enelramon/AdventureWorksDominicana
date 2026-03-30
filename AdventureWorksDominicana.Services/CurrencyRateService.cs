using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdventureWorksDominicana.Services;

public class CurrencyRateService(IDbContextFactory<Contexto> DbFactory) : IService<CurrencyRate, int>
{
    public Task<CurrencyRate?> Buscar(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Eliminar(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<CurrencyRate>> GetList(Expression<Func<CurrencyRate, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.CurrencyRates.Where(criterio).AsNoTracking().ToListAsync();
    }

    public Task<bool> Guardar(CurrencyRate entidad)
    {
        throw new NotImplementedException();
    }
}
