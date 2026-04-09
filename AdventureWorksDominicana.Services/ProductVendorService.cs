using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdventureWorksDominicana.Services;

public class ProductVendorService(IDbContextFactory<Contexto> DbFactory) : IService<ProductVendor, int>
{
    public Task<ProductVendor?> Buscar(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Eliminar(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ProductVendor>> GetList(Expression<Func<ProductVendor, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ProductVendors.Include(p => p.Product).ThenInclude(p => p.ProductSubcategory).ThenInclude(p => p.ProductCategory)
         .Include(p => p.Product).ThenInclude(s => s.SpecialOfferProducts).ThenInclude(s => s.SpecialOffer)
         .Include(p => p.Product).ThenInclude(p => p.ProductModel).ThenInclude(d => d.ProductModelProductDescriptionCultures).ThenInclude(d => d.ProductDescription)
         .Where(criterio).ToListAsync();
    }

    public Task<bool> Guardar(ProductVendor entidad)
    {
        throw new NotImplementedException();
    }
}
