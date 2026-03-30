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
            .Include(p => p.ProductSubcategory)
            .Include(p => p.ProductModel)
            .Include(p => p.SizeUnitMeasureCodeNavigation)
            .Include(p => p.WeightUnitMeasureCodeNavigation).Where(criterio).ToListAsync();
    }
    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.Products.AnyAsync(p => p.ProductId == id);
    }

    public async Task<bool> Guardar(Product product)
    {
        if (!await Existe(product.ProductId))
        {
            return await Insertar(product);
        }
        else
        {
            return await Modificar(product);
        }

    }
    public async Task<bool> Insertar(Product product)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        if (!await UnicidadNombreONumeroOkAsync(contexto, product))
        {
            return false;
        }
        product.Rowguid = Guid.NewGuid();
        product.ModifiedDate = DateTime.Now;

        contexto.Products.Add(product);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Modificar(Product product)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        if (!await UnicidadNombreONumeroOkAsync(contexto, product))
        {
            return false;
        }

        product.ModifiedDate = DateTime.Now;
        product.ProductSubcategory = null;
        product.ProductModel = null;
        product.SizeUnitMeasureCodeNavigation = null;
        product.WeightUnitMeasureCodeNavigation = null;

        contexto.Products.Update(product);
        return await contexto.SaveChangesAsync() > 0;
    }
    private static async Task<bool> UnicidadNombreONumeroOkAsync(Contexto contexto, Product product)
    {
        var duplicado = await contexto.Products.AnyAsync(p => p.ProductId != product.ProductId && (p.Name == product.Name || p.ProductNumber == product.ProductNumber));
        return !duplicado;
    }
    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        try
        {
            var filas = await contexto.Products.Where(p => p.ProductId == id).ExecuteDeleteAsync();
            return filas > 0;
        }
        catch (DbUpdateException)
        {
            return false;
        }
        catch
        {
            return false;
        }
    }
}