using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdventureWorksDominicana.Services;

public class AddressService(IDbContextFactory<Contexto> DbFactory) : IService<Address, int>
{
    public Task<Address?> Buscar(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Eliminar(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Address>> GetList(Expression<Func<Address, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Addresses.Where(criterio).Include(s => s.StateProvince).ThenInclude(s => s.SalesTaxRates).OrderBy(a => a.City).AsNoTracking().ToListAsync();
    }

    public Task<bool> Guardar(Address entidad)
    {
        throw new NotImplementedException();
    }
}
