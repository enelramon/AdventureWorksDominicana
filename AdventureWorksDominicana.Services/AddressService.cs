using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Services;

public class AddressService
{
    private readonly IDbContextFactory<Contexto> _db;

    public AddressService(IDbContextFactory<Contexto> db)
    {
        _db = db;
    }

    public async Task<List<Address>> Listar(Func<Address, bool> filtro)
    {
        await using var ctx = await _db.CreateDbContextAsync();

        return ctx.Addresses
            .Include(x => x.StateProvince)
            .AsNoTracking()
            .Where(filtro)
            .OrderBy(x => x.AddressId)
            .ToList();
    }

    public async Task<Address?> Buscar(int id)
    {
        await using var ctx = await _db.CreateDbContextAsync();
        return await ctx.Addresses.FindAsync(id);
    }

    public async Task<bool> Guardar(Address entity)
    {
        await using var ctx = await _db.CreateDbContextAsync();

        entity.StateProvince = null;

        if (entity.AddressId == 0)
            ctx.Addresses.Add(entity);
        else
            ctx.Addresses.Update(entity);

        return await ctx.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var ctx = await _db.CreateDbContextAsync();

        var entity = await ctx.Addresses.FindAsync(id);
        if (entity == null) return false;

        ctx.Addresses.Remove(entity);
        return await ctx.SaveChangesAsync() > 0;
    }

    public async Task<List<StateProvince>> GetProvincias()
    {
        await using var ctx = await _db.CreateDbContextAsync();

        return await ctx.StateProvinces
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync();
    }
}
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
        return await contexto.Addresses.Include(s => s.StateProvince).ThenInclude(t => t.SalesTaxRates).Where(criterio).AsNoTracking().ToListAsync();
    }

    public Task<bool> Guardar(Address entidad)
    {
        throw new NotImplementedException();
    }
}
