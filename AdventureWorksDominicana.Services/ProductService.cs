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
        return await contexto.Products
            .Include(p => p.ProductSubcategory).ThenInclude(p => p.ProductCategory)
            .Include(p => p.ProductModel).ThenInclude(d => d.ProductModelProductDescriptionCultures).ThenInclude(d => d.ProductDescription)
            .Include(p => p.SizeUnitMeasureCodeNavigation)
            .Include(p => p.WeightUnitMeasureCodeNavigation)
            .Include(p => p.ProductProductPhotos).ThenInclude(ppp => ppp.ProductPhoto)
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<List<Product>> GetListIndex(Expression<Func<Product, bool>> criterio)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.Products.AsNoTracking().Include(p => p.ProductSubcategory).Where(criterio).ToListAsync();
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
        
        var productoOriginal = await contexto.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == product.ProductId);

        if (productoOriginal == null) return false;

        product.ModifiedDate = DateTime.Now;

        if (productoOriginal.ListPrice != product.ListPrice)
        {
            contexto.ProductListPriceHistories.Add(new ProductListPriceHistory
            {
                ProductId = product.ProductId,
                StartDate = DateTime.Now,
                ListPrice = product.ListPrice,
                ModifiedDate = DateTime.Now
            });
        }

        if (productoOriginal.StandardCost != product.StandardCost)
        {
            contexto.ProductCostHistories.Add(new ProductCostHistory
            {
                ProductId = product.ProductId,
                StartDate = DateTime.Now,
                StandardCost = product.StandardCost,
                ModifiedDate = DateTime.Now
            });
        }

        contexto.Entry(product).State = EntityState.Modified;
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        try
        {
            return await contexto.Products.Where(p => p.ProductId == id).ExecuteDeleteAsync() > 0;
        }
        catch (Exception) 
        {
            var producto = await contexto.Products.FindAsync(id);
            if (producto != null)
            {
                producto.SellEndDate = DateTime.Now;
                contexto.Products.Update(producto);
                await contexto.SaveChangesAsync();
                return true; 
            }
            return false;
        }
    }

    public async Task<List<Product>> GetTopFeaturedProducts(int cantidad = 4)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();

        return await contexto.Products
            .AsNoTracking()
            .Where(p => p.SellStartDate <= DateTime.Now)
            .OrderByDescending(p => p.ListPrice)
            .Take(cantidad)
            .Include(p => p.ProductSubcategory)
                .ThenInclude(ps => ps.ProductCategory)
            .Include(p => p.ProductProductPhotos)
                .ThenInclude(ppp => ppp.ProductPhoto)
            .ToListAsync();
    }

}

public class ProductDependentDataException : Exception
{
    public ProductDependentDataException() : base() { }
    public ProductDependentDataException(string message) : base(message) { }
    public ProductDependentDataException(string message, Exception innerException) : base(message, innerException) { }
}