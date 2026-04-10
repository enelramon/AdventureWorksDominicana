using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class PurchaseOrderService(IDbContextFactory<Contexto> DbFactory) : IService<PurchaseOrderHeader, int>
{
    public async Task<List<PurchaseOrderHeader>> Listar(Expression<Func<PurchaseOrderHeader, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.PurchaseOrderHeaders
            .Include(p => p.Vendor)
            .Include(p => p.ShipMethod)
            .Include(p => p.PurchaseOrderDetails)
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> Guardar(PurchaseOrderHeader purchaseOrder)
    {
        if (!await Existe(purchaseOrder.PurchaseOrderId))
            return await Insertar(purchaseOrder);
        else
            return await Modificar(purchaseOrder);
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.PurchaseOrderHeaders.AnyAsync(p => p.PurchaseOrderId == id);
    }

    private async Task<bool> Insertar(PurchaseOrderHeader purchaseOrder)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        purchaseOrder.ModifiedDate = DateTime.Now;
        contexto.PurchaseOrderHeaders.Add(purchaseOrder);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(PurchaseOrderHeader purchaseOrder)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        purchaseOrder.ModifiedDate = DateTime.Now;
        contexto.PurchaseOrderHeaders.Update(purchaseOrder);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<PurchaseOrderHeader?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.PurchaseOrderHeaders
            .Include(p => p.Vendor)
            .Include(p => p.ShipMethod)
            .Include(p => p.PurchaseOrderDetails)
            .FirstOrDefaultAsync(p => p.PurchaseOrderId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.PurchaseOrderHeaders
            .Where(p => p.PurchaseOrderId == id)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<PurchaseOrderHeader>> GetList(Expression<Func<PurchaseOrderHeader, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.PurchaseOrderHeaders
            .Include(p => p.Vendor)
            .Include(p => p.ShipMethod)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<List<Product>> ListarProductos()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Products.AsNoTracking().ToListAsync();
    }

    public async Task<List<Vendor>> ListarVendors()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Vendors.AsNoTracking().ToListAsync();
    }

    public async Task<List<ShipMethod>> ListarShipMethods()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ShipMethods.AsNoTracking().ToListAsync();
    }
}