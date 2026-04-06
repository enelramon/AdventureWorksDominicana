using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdventureWorksDominicana.Services;

public class SalesOrderHeaderService(IDbContextFactory<Contexto> DbFactory) : IService<SalesOrderHeader, int>
{
    public async Task<bool> Guardar(SalesOrderHeader sale)
    {
        if (!await Existe(sale.SalesOrderId))
            return await Insertar(sale);
        else
            return await Modificar(sale);
    }
    private async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.SalesOrderHeaders.AnyAsync(s => s.SalesOrderId == id);
    }
    private async Task<bool> Insertar(SalesOrderHeader sale)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        foreach(var detalle in sale.SalesOrderDetails)
        {
            var productInventory = await contexto.ProductInventories.FirstOrDefaultAsync(i => i.ProductId == detalle.ProductId);
            if(productInventory != null)
            {
                productInventory.Quantity -= detalle.OrderQty;
                productInventory.ModifiedDate = DateTime.Now;
            }
        }
        contexto.SalesOrderHeaders.Add(sale); 
        return await contexto.SaveChangesAsync() > 0;
    }
    private async Task<bool> Modificar(SalesOrderHeader sale)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var modificatedSale = await contexto.SalesOrderHeaders.Include(d => d.SalesOrderDetails).FirstOrDefaultAsync(s => s.SalesOrderId == sale.SalesOrderId);
        if (modificatedSale == null) return false;
        foreach (var detalle in modificatedSale.SalesOrderDetails)
        {
            var productInventory = await contexto.ProductInventories.FirstOrDefaultAsync(i => i.ProductId == detalle.ProductId);
            if (productInventory != null)
            {
                productInventory.Quantity += detalle.OrderQty;
            }
        }
        contexto.SalesOrderDetails.RemoveRange(modificatedSale.SalesOrderDetails);
        contexto.Entry(modificatedSale).CurrentValues.SetValues(sale);
        modificatedSale.ModifiedDate = DateTime.Now;
        foreach (var newDetalle in sale.SalesOrderDetails)
        {
            modificatedSale.SalesOrderDetails.Add(
                new SalesOrderDetail
                {
                    CarrierTrackingNumber = newDetalle.CarrierTrackingNumber,
                    OrderQty = newDetalle.OrderQty,
                    ProductId = newDetalle.ProductId,
                    SpecialOfferId = newDetalle.SpecialOfferId,
                    UnitPrice = newDetalle.UnitPrice,
                    UnitPriceDiscount = newDetalle.UnitPriceDiscount,
                    LineTotal = newDetalle.LineTotal,
                    Rowguid = newDetalle.Rowguid,
                    ModifiedDate = DateTime.Now
                }
            );
            var productInventory = await contexto.ProductInventories.FirstOrDefaultAsync(i => i.ProductId == newDetalle.ProductId);
            if (productInventory != null)
            {
                productInventory.Quantity -= newDetalle.OrderQty;
                productInventory.ModifiedDate = DateTime.Now;
            }
        }
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<SalesOrderHeader?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.SalesOrderHeaders.Include(s => s.SalesOrderDetails).Include(d => d.SalesOrderDetails).ThenInclude(p => p.SpecialOfferProduct).ThenInclude(p => p.Product).ThenInclude(p => p.ProductSubcategory).ThenInclude(p => p.ProductCategory).FirstOrDefaultAsync(s => s.SalesOrderId == id);
    }
    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var sale = contexto.SalesOrderHeaders.Include(s => s.SalesOrderDetails).FirstOrDefault(s => s.SalesOrderId == id);
        if(sale != null)
        {
            foreach (var detalle in sale.SalesOrderDetails)
            {
                var productInventory = await contexto.ProductInventories.FirstOrDefaultAsync(i => i.ProductId == detalle.ProductId);
                if (productInventory != null)
                {
                    productInventory.Quantity += detalle.OrderQty;
                    productInventory.ModifiedDate = DateTime.Now;
                    contexto.ProductInventories.Update(productInventory);
                }
            }
            contexto.SalesOrderDetails.RemoveRange(sale.SalesOrderDetails);
            contexto.SalesOrderHeaders.Remove(sale);
        }
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<SalesOrderHeader>> GetList(Expression<Func<SalesOrderHeader, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.SalesOrderHeaders.Where(criterio).AsNoTracking().ToListAsync();
    }
}
