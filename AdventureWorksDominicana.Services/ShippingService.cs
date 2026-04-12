using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class ShippingService(IDbContextFactory<Contexto> DbContextFactory)
    : IService<SalesOrderHeader, int>
{
    public async Task<bool> Existe(int id)
    {
        await using var context = await DbContextFactory.CreateDbContextAsync();
        return await context.SalesOrderHeaders.AnyAsync(x => x.SalesOrderId == id);
    }

    public async Task<bool> Guardar(SalesOrderHeader entity)
    {
        await using var context = await DbContextFactory.CreateDbContextAsync();
        context.SalesOrderHeaders.Add(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(SalesOrderHeader entity)
    {
        await using var context = await DbContextFactory.CreateDbContextAsync();
        context.SalesOrderHeaders.Update(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var context = await DbContextFactory.CreateDbContextAsync();
        var entity = await context.SalesOrderHeaders.FindAsync(id);
        if (entity == null) return false;

        context.SalesOrderHeaders.Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<SalesOrderHeader?> Buscar(int id)
    {
        await using var context = await DbContextFactory.CreateDbContextAsync();

        return await context.SalesOrderHeaders
            .AsNoTracking()
            .Include(o => o.Customer)
                .ThenInclude(c => c.Person)
            .Include(o => o.Customer)
                .ThenInclude(c => c.Store)
            .Include(o => o.ShipMethod)
            .Include(o => o.ShipToAddress)
            .Include(o => o.BillToAddress)
            .Include(o => o.SalesOrderDetails)
                .ThenInclude(d => d.SpecialOfferProduct)
                    .ThenInclude(s => s.Product)
            .FirstOrDefaultAsync(o => o.SalesOrderId == id);
    }

    public async Task<List<SalesOrderHeader>> GetList(Expression<Func<SalesOrderHeader, bool>> criterio)
    {
        await using var context = await DbContextFactory.CreateDbContextAsync();

        return await context.SalesOrderHeaders
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }


    public async Task<List<SalesOrderHeader>> GetShippingOrdersAsync(DateTime? from, DateTime? to, string? search)
    {
        await using var context = await DbContextFactory.CreateDbContextAsync();

        var query = context.SalesOrderHeaders
            .AsNoTracking()
            .Include(o => o.Customer).ThenInclude(c => c.Person)
            .Include(o => o.Customer).ThenInclude(c => c.Store)
            .Include(o => o.ShipMethod)
            .Include(o => o.ShipToAddress)
            .Where(o => o.Status == 1 || o.Status == 5);

        if (from.HasValue)
            query = query.Where(o => o.OrderDate >= from.Value.Date);

        if (to.HasValue)
            query = query.Where(o => o.OrderDate < to.Value.Date.AddDays(1));

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();

            query = query.Where(o =>
                o.SalesOrderId.ToString().Contains(search) ||
                (o.Customer.Person != null &&
                    ((o.Customer.Person.FirstName ?? "") + " " +
                     (o.Customer.Person.LastName ?? "")).Contains(search)) ||
                (o.Customer.Store != null &&
                    (o.Customer.Store.Name ?? "").Contains(search))
            );
        }

        return await query.OrderByDescending(o => o.OrderDate).ToListAsync();
    }

    public async Task<List<ShipMethod>> GetShipMethodsAsync()
    {
        await using var context = await DbContextFactory.CreateDbContextAsync();

        return await context.ShipMethods
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<decimal> CalculateFreightAsync(int shipMethodId, decimal subtotal)
    {
        await using var context = await DbContextFactory.CreateDbContextAsync();

        var shipMethod = await context.ShipMethods
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ShipMethodId == shipMethodId);

        if (shipMethod == null)
            throw new InvalidOperationException("Método de envío no existe.");

        return Math.Round(shipMethod.ShipBase + (subtotal * shipMethod.ShipRate), 2);
    }

    public async Task ConfirmShipmentAsync(int orderId, int shipMethodId)
    {
        await using var context = await DbContextFactory.CreateDbContextAsync();

        var order = await context.SalesOrderHeaders
            .FirstOrDefaultAsync(o => o.SalesOrderId == orderId);

        if (order == null)
            throw new InvalidOperationException("Orden no encontrada.");

        if (order.Status == 5)
            throw new InvalidOperationException("Ya está enviada.");

        var subtotal = await context.SalesOrderDetails
            .Where(d => d.SalesOrderId == orderId)
            .SumAsync(d => d.LineTotal);

        var freight = await CalculateFreightAsync(shipMethodId, subtotal);

        order.ShipMethodId = shipMethodId;
        order.ShipDate = DateTime.Now;
        order.ModifiedDate = DateTime.Now;
        order.SubTotal = subtotal;
        order.Freight = freight;
        order.TotalDue = subtotal + order.TaxAmt + freight;
        order.Status = 5;

        await context.SaveChangesAsync();
    }

    public async Task UpdateShipmentAsync(int orderId, int shipMethodId, string? comment)
    {
        await using var context = await DbContextFactory.CreateDbContextAsync();

        var order = await context.SalesOrderHeaders
            .FirstOrDefaultAsync(o => o.SalesOrderId == orderId);

        if (order == null)
            throw new InvalidOperationException("Orden no encontrada.");

        if (order.Status != 5)
            throw new InvalidOperationException("Solo envíos confirmados.");

        var subtotal = await context.SalesOrderDetails
            .Where(d => d.SalesOrderId == orderId)
            .SumAsync(d => d.LineTotal);

        var freight = await CalculateFreightAsync(shipMethodId, subtotal);

        order.ShipMethodId = shipMethodId;
        order.Comment = comment;
        order.ModifiedDate = DateTime.Now;
        order.SubTotal = subtotal;
        order.Freight = freight;
        order.TotalDue = subtotal + order.TaxAmt + freight;

        await context.SaveChangesAsync();
    }
}