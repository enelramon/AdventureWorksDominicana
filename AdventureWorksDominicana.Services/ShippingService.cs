using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Services;

public class ShippingService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<List<ShipMethodResumen>> GetResumenPorMetodo()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var metodos = await contexto.ShipMethods.AsNoTracking().ToListAsync();

        var ventasPorMetodo = await contexto.SalesOrderHeaders
            .AsNoTracking()
            .GroupBy(s => s.ShipMethodId)
            .Select(g => new
            {
                ShipMethodId = g.Key,
                TotalVentas = g.Count(),
                FleteVentas = g.Sum(x => x.Freight)
            })
            .ToListAsync();

        var comprasPorMetodo = await contexto.PurchaseOrderHeaders
            .AsNoTracking()
            .GroupBy(p => p.ShipMethodId)
            .Select(g => new
            {
                ShipMethodId = g.Key,
                TotalCompras = g.Count(),
                FleteCompras = g.Sum(x => x.Freight)
            })
            .ToListAsync();

        return metodos.Select(m => new ShipMethodResumen
        {
            ShipMethodId = m.ShipMethodId,
            Name = m.Name,
            ShipBase = m.ShipBase,
            ShipRate = m.ShipRate,
            TotalVentas = ventasPorMetodo.FirstOrDefault(v => v.ShipMethodId == m.ShipMethodId)?.TotalVentas ?? 0,
            FleteVentas = ventasPorMetodo.FirstOrDefault(v => v.ShipMethodId == m.ShipMethodId)?.FleteVentas ?? 0,
            TotalCompras = comprasPorMetodo.FirstOrDefault(c => c.ShipMethodId == m.ShipMethodId)?.TotalCompras ?? 0,
            FleteCompras = comprasPorMetodo.FirstOrDefault(c => c.ShipMethodId == m.ShipMethodId)?.FleteCompras ?? 0
        }).ToList();
    }

    public async Task<List<SalesOrderHeader>> GetVentasEnTransito()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.SalesOrderHeaders
            .AsNoTracking()
            .Include(s => s.Customer).ThenInclude(c => c.Person)
            .Include(s => s.Customer).ThenInclude(c => c.Store)
            .Include(s => s.ShipMethod)
            .Where(s => s.Status >= 1 && s.Status <= 4)
            .OrderByDescending(s => s.OrderDate)
            .ToListAsync();
    }

    public async Task<List<PurchaseOrderHeader>> GetComprasEnTransito()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.PurchaseOrderHeaders
            .AsNoTracking()
            .Include(p => p.Vendor)
            .Include(p => p.ShipMethod)
            .Where(p => p.Status == 1 || p.Status == 2)
            .OrderByDescending(p => p.OrderDate)
            .ToListAsync();
    }

    public async Task<List<PurchaseOrderHeader>> GetHistoricoCompras()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.PurchaseOrderHeaders
            .AsNoTracking()
            .Include(p => p.Vendor)
            .Include(p => p.ShipMethod)
            .Where(p => p.Status == 4 && p.ShipDate != null)
            .OrderByDescending(p => p.ShipDate)
            .ToListAsync();
    }
}

public class ShipMethodResumen
{
    public int ShipMethodId { get; set; }
    public string Name { get; set; } = "";
    public decimal ShipBase { get; set; }
    public decimal ShipRate { get; set; }
    public int TotalVentas { get; set; }
    public decimal FleteVentas { get; set; }
    public int TotalCompras { get; set; }
    public decimal FleteCompras { get; set; }

    public int TotalOrdenes => TotalVentas + TotalCompras;
    public decimal FleteTotal => FleteVentas + FleteCompras;
}