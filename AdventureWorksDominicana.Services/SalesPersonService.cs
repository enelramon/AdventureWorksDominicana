using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdventureWorksDominicana.Services;

public class SalesPersonService(IDbContextFactory<Contexto> DbFactory) : IService<SalesPerson, int>
{
    public Task<SalesPerson?> Buscar(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Eliminar(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<SalesPerson>> GetList(Expression<Func<SalesPerson, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.SalesPeople.Where(criterio).AsNoTracking().ToListAsync();
    }

    public Task<bool> Guardar(SalesPerson entidad)
    {
        throw new NotImplementedException();
    }
}
