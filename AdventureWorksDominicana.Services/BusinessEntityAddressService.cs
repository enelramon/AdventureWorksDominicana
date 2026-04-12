using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdventureWorksDominicana.Services;

public class BusinessEntityAddressService(IDbContextFactory<Contexto> DbFactory) : IService<BusinessEntityAddress, int>
{
    public async Task<bool> Guardar(BusinessEntityAddress entidad)
    {
        if (!await Existe(entidad.BusinessEntityId, entidad.AddressId))
            return await Insertar(entidad);
        else
            return await Modificar(entidad);
    }

    private async Task<bool> Existe(int businessId, int addressId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.BusinessEntityAddresses.AnyAsync(b => b.BusinessEntityId == businessId && b.AddressId == addressId);
    }

    private async Task<bool> Insertar(BusinessEntityAddress businessEntityAddress)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.BusinessEntityAddresses.Add(businessEntityAddress);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(BusinessEntityAddress businessEntityAddress)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(businessEntityAddress);
        return await contexto.SaveChangesAsync() > 0;
    }
    public Task<BusinessEntityAddress?> Buscar(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Eliminar(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<BusinessEntityAddress>> GetList(Expression<Func<BusinessEntityAddress, bool>> criterio)
    {
        throw new NotImplementedException();
    }

   
}
