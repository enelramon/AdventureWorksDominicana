using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class ProductService(IDbContextFactory<Contexto> DbContextFactory) : IService<Product, int>
{
    public async Task<Product?> Buscar(int id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.Products.AsNoTracking()
            .Include(p => p.ProductSubcategory)
            .Include(p => p.ProductModel)
            .Include(p => p.SizeUnitMeasureCodeNavigation)
            .Include(p => p.WeightUnitMeasureCodeNavigation)
            .FirstOrDefaultAsync(p => p.ProductId == id);
    }

    public async Task<List<Product>> GetList(Expression<Func<Product, bool>> criterio)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.Products.AsNoTracking()
            .Include(p => p.ProductSubcategory).ThenInclude(p => p.ProductCategory)
            .Include(p => p.ProductModel).ThenInclude(d => d.ProductModelProductDescriptionCultures).ThenInclude(d => d.ProductDescription)
            .Include(p => p.ProductModel).ThenInclude(d => d.ProductModelProductDescriptionCultures).ThenInclude(c => c.Culture)
            .Include(p => p.SizeUnitMeasureCodeNavigation)
            .Include(p => p.WeightUnitMeasureCodeNavigation)
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.Products.AnyAsync(p => p.ProductId == id);
    }

    public async Task<bool> Guardar(Product product)
    {
        if (!await Existe(product.ProductId))
            return await Insertar(product);

        return await Modificar(product);
    }

    public async Task<bool> Insertar(Product product)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        product.Rowguid = Guid.NewGuid();
        product.ModifiedDate = DateTime.Now;

        contexto.Products.Add(product);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Product product)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        product.ModifiedDate = DateTime.Now;
        product.ProductSubcategory = null;
        product.ProductModel = null;
        product.SizeUnitMeasureCodeNavigation = null;
        product.WeightUnitMeasureCodeNavigation = null;

        contexto.Products.Update(product);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        try
        {
            return await contexto.Products.Where(p => p.ProductId == id).ExecuteDeleteAsync() > 0;
        }
        catch { return false; }
    }
}