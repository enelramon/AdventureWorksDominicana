using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class ShippingService(IDbContextFactory<Contexto> DbFactory)
    : IService<SalesOrderHeader, int>
{
    public async Task<bool> Guardar(SalesOrderHeader entity)
    {
        if (!await Existe(entity.SalesOrderId))
            return await Insertar(entity);
        else
            return await Modificar(entity);
    }

    public async Task<bool> Insertar(SalesOrderHeader entity)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.SalesOrderHeaders.Add(entity);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.SalesOrderHeaders.AnyAsync(x => x.SalesOrderId == id);
    }

    public async Task<bool> Modificar(SalesOrderHeader entity)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.SalesOrderHeaders.Update(entity);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<SalesOrderHeader?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.SalesOrderHeaders
            .Include(x => x.Customer)
                .ThenInclude(c => c.Person)
            .Include(x => x.Customer)
                .ThenInclude(c => c.Store)
            .Include(x => x.SalesOrderDetails)
                .ThenInclude(d => d.SpecialOfferProduct)
                    .ThenInclude(s => s.Product)
            .FirstOrDefaultAsync(x => x.SalesOrderId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var entity = await Buscar(id);

        if (entity == null) return false;

        contexto.SalesOrderHeaders.Remove(entity);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<SalesOrderHeader>> GetList(Expression<Func<SalesOrderHeader, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var rawData = await contexto.SalesOrderHeaders
            .AsNoTracking()
            .Where(criterio)
            .OrderByDescending(x => x.OrderDate)
            .Select(x => new
            {
                x.SalesOrderId,
                x.OrderDate,
                x.TotalDue,
                Freight = (decimal?)x.Freight,
                Status = (byte?)x.Status,

                FirstName = x.Customer != null && x.Customer.Person != null ? x.Customer.Person.FirstName : "",
                LastName = x.Customer != null && x.Customer.Person != null ? x.Customer.Person.LastName : "",
                StoreName = x.Customer != null && x.Customer.Store != null ? x.Customer.Store.Name : ""
            })
            .ToListAsync();

        return rawData.Select(x => new SalesOrderHeader
        {
            SalesOrderId = x.SalesOrderId,
            OrderDate = x.OrderDate,
            TotalDue = x.TotalDue,
            Freight = x.Freight ?? 0,
            Status = x.Status ?? 0,
            Customer = new Customer
            {
                Person = string.IsNullOrWhiteSpace(x.FirstName)
                    ? null
                    : new Person { FirstName = x.FirstName, LastName = x.LastName },
                Store = string.IsNullOrWhiteSpace(x.StoreName)
                    ? null
                    : new Store { Name = x.StoreName }
            }
        }).ToList();
    }

    public async Task<bool> ProcesarEnvio(int orderId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var order = await contexto.SalesOrderHeaders
            .Include(x => x.SalesOrderDetails)
            .FirstOrDefaultAsync(x => x.SalesOrderId == orderId);

        if (order == null) return false;

        decimal pesoTotal = order.SalesOrderDetails.Sum(x => x.OrderQty);

        order.Freight = pesoTotal * 2;
        order.Status = 5;

        contexto.Update(order);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<decimal> GetPesoTotal(int orderId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.SalesOrderDetails
            .Where(x => x.SalesOrderId == orderId)
            .SumAsync(x => (decimal)x.OrderQty);
    }

    public async Task<bool> CancelarEnvio(int orderId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var order = await contexto.SalesOrderHeaders
            .FirstOrDefaultAsync(x => x.SalesOrderId == orderId);

        if (order == null) return false;

        order.Status = 6;

        contexto.Update(order);

        return await contexto.SaveChangesAsync() > 0;
    }
}