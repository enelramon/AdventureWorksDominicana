using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksDominicana.Services;

public class SpecialOfferProductService(IDbContextFactory<Contexto> DbFactory)
{
    // EXPLICACIÓN: Implementamos Guardar con validación de existencia única
    public async Task<bool> Guardar(SpecialOfferProduct entidad)
    {
        if (!await Existe(entidad.SpecialOfferId, entidad.ProductId))
            return await Insertar(entidad);

        return await Modificar(entidad);
    }

    // EXPLICACIÓN: Método para verificar si la relación ya existe (Llave Compuesta)
    public async Task<bool> Existe(int specialOfferId, int productId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.SpecialOfferProducts
            .AnyAsync(s => s.SpecialOfferId == specialOfferId && s.ProductId == productId);
    }

    // EXPLICACIÓN: Inserción con generación de metadatos obligatorios (UTC y GUID)
    private async Task<bool> Insertar(SpecialOfferProduct entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        entidad.ModifiedDate = DateTime.UtcNow; // Mejor UTC para evitar líos en el servidor
        entidad.Rowguid = Guid.NewGuid(); // Requerido por AdventureWorks

        contexto.SpecialOfferProducts.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(SpecialOfferProduct entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        entidad.ModifiedDate = DateTime.UtcNow;

        contexto.SpecialOfferProducts.Update(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    // EXPLICACIÓN: ExecuteDelete para una eliminación rápida por Llave Compuesta
    public async Task<bool> Eliminar(int specialOfferId, int productId)
    {
        try
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.SpecialOfferProducts
                .Where(s => s.SpecialOfferId == specialOfferId && s.ProductId == productId)
                .ExecuteDeleteAsync() > 0;
        }
        catch (DbUpdateException)
        {
            // Falla si el registro está siendo usado en una venta (SalesOrderDetail)
            return false;
        }
    }

    // EXPLICACIÓN: Búsqueda específica que no estaba en el código original
    public async Task<SpecialOfferProduct?> Buscar(int specialOfferId, int productId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.SpecialOfferProducts
            .AsNoTracking()
            .Include(o => o.SpecialOffer)
            .Include(p => p.Product)
            .FirstOrDefaultAsync(s => s.SpecialOfferId == specialOfferId && s.ProductId == productId);
    }

    // EXPLICACIÓN: GetList optimizado con navegación completa
    public async Task<List<SpecialOfferProduct>> GetList(Expression<Func<SpecialOfferProduct, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.SpecialOfferProducts
            .AsNoTracking()
            .Include(o => o.SpecialOffer)
            .Include(p => p.Product)
                .ThenInclude(i => i.ProductInventories)
            .Where(criterio)
            .ToListAsync();
    }
}