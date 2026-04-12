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
    public async Task<bool> Guardar(Address address)
    {
        if (!await Existe(address.AddressId))
            return await Insertar(address);
        else
            return await Modificar(address);
    }

    private async Task<bool> Existe(int addressId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Addresses.AnyAsync(a => a.AddressId == addressId);
    }

    private async Task<bool> Insertar(Address address)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Addresses.Add(address);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Address address)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(address);
        return await contexto.SaveChangesAsync() > 0;
    }
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
        return await contexto.Addresses.Include(s => s.StateProvince).ThenInclude(t => t.SalesTaxRates).Include(b => b.BusinessEntityAddresses).Where(criterio).AsNoTracking().ToListAsync();
    }

    
}
