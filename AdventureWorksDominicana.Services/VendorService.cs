using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdventureWorksDominicana.Services;

public class VendorService(IDbContextFactory<Contexto> DbContextFactory) : IService<Vendor, int>
{
    public async Task<Vendor?> Buscar(int id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.Vendors.FirstOrDefaultAsync(v => v.BusinessEntityId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        bool enUso = await contexto.PurchaseOrderHeaders.AnyAsync(p => p.VendorId == id);

        if (enUso)
        {
            throw new InvalidOperationException("No se puede eliminar el proveedor porque tiene órdenes asociadas.");
        }
        var filasAfectadas = await contexto.Vendors.Where(v => v.BusinessEntityId == id).ExecuteDeleteAsync();
        return filasAfectadas > 0;

    }

    public async Task<List<Vendor>> GetList(Expression<Func<Vendor, bool>> criterio)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.Vendors.Where(criterio).AsNoTracking().ToListAsync();
    }

    public async Task<bool> Guardar(Vendor vendor)
    {
        if (!await Existe(vendor.BusinessEntityId))
        {
            return await Insertar(vendor);
        }
        else
        {
            return await Modificar(vendor);
        }
    }

    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.Vendors.AnyAsync(v => v.BusinessEntityId == id);
    }
    public async Task<bool> Insertar(Vendor vendor)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        var nuevaEntidad = new BusinessEntity
        {
            ModifiedDate = DateTime.Now,
            Rowguid = Guid.NewGuid()
        };

        contexto.BusinessEntities.Add(nuevaEntidad);


        var paso1 = await contexto.SaveChangesAsync() > 0;

        if (paso1)
        {
            vendor.BusinessEntityId = nuevaEntidad.BusinessEntityId;
            vendor.ModifiedDate = DateTime.Now;

            contexto.Vendors.Add(vendor);

            return await contexto.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> Modificar(Vendor vendor)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        vendor.ModifiedDate = DateTime.Now;
        contexto.Vendors.Update(vendor);

        return await contexto.SaveChangesAsync() > 0;
    }
}
