using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdventureWorksDominicana.Services;

public class SalesTerritoryService(IDbContextFactory<Contexto> DbFactory) : IService<SalesTerritory, int>
{
    public async Task<bool> Guardar(SalesTerritory entidad)
    {
        if (!await Existe(entidad.TerritoryId))
        {
            return await Insertar(entidad);
        }
        else
        {
            return await Modificar(entidad);
        }
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.SalesTerritories.AnyAsync(s => s.TerritoryId == id);
    }

    private async Task<bool> Insertar(SalesTerritory entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.SalesTerritories.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(SalesTerritory entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.SalesTerritories.Update(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<SalesTerritory?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.SalesTerritories.FirstOrDefaultAsync(s => s.TerritoryId == id);
    }

    public async Task<bool> BuscarDuplicado(string nombre, string paisCodigo, int idExcluido = 0)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.SalesTerritories.AnyAsync(s =>s.CountryRegionCode.ToLower().Equals(paisCodigo) && s.Name.ToLower().Equals(nombre) && s.TerritoryId != idExcluido);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var existe = await contexto.SalesTerritories.AnyAsync(s => s.TerritoryId == id);
        if (!existe)
        {
            throw new InvalidOperationException("No se puede eliminar: el territorio no existe");
        }

        var tieneEstados = await contexto.StateProvinces.AnyAsync(s => s.TerritoryId == id);
        if (tieneEstados)
        {
            throw new InvalidOperationException("No se puede eliminar: el territorio tiene estados/provincias asociados");
        }

        var tieneOrdenes = await contexto.SalesOrderHeaders.AnyAsync(o => o.TerritoryId == id);
        if (tieneOrdenes)
        {
            throw new InvalidOperationException("No se puede eliminar: el territorio tiene órdenes de venta asociadas");
        }

        var tieneVendedores = await contexto.SalesPeople.AnyAsync(p => p.TerritoryId == id);
        if (tieneVendedores)
        {
            throw new InvalidOperationException("No se puede eliminar: el territorio tiene vendedores asignados");
        }

        var tieneClientes = await contexto.Customers.AnyAsync(c => c.TerritoryId == id);
        if (tieneClientes)
        {
            throw new InvalidOperationException("No se puede eliminar: el territorio tiene clientes asignados");
        }

        return await contexto.SalesTerritories.Where(s => s.TerritoryId == id).ExecuteDeleteAsync() > 0;
    }

    public async Task<List<SalesTerritory>> GetList(Expression<Func<SalesTerritory, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.SalesTerritories.Where(criterio).OrderBy(t => t.Name).ToListAsync();
    }
}
