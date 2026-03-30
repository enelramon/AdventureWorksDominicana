using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class ProductSubcategoryService(IDbContextFactory<Contexto> DbContextFactory) : IService<ProductSubcategory, int>
{
    public async Task<ProductSubcategory?> Buscar(int id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.ProductSubcategories.Include(s => s.ProductCategory).FirstOrDefaultAsync(s => s.ProductSubcategoryId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        var filasAfectadas = await contexto.ProductSubcategories.Where(s => s.ProductSubcategoryId == id).ExecuteDeleteAsync();
        return filasAfectadas > 0;
    }
    public async Task<List<ProductSubcategory>> GetList(Expression<Func<ProductSubcategory, bool>> criterio)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.ProductSubcategories.Include(s => s.ProductCategory).Where(criterio).AsNoTracking().ToListAsync();
    }
    public async Task<bool> Guardar(ProductSubcategory subcategory)
    {
        if (!await Existe(subcategory.ProductSubcategoryId))
        {
            return await Insertar(subcategory);

        }
        else
        {
            return await Modificar(subcategory);
        }
    }
    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.ProductSubcategories.AnyAsync(s => s.ProductSubcategoryId == id);
    }
    public async Task<bool> Insertar(ProductSubcategory subcategory)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        subcategory.Rowguid = Guid.NewGuid();
        subcategory.ModifiedDate = DateTime.Now;
        contexto.ProductSubcategories.Add(subcategory);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Modificar(ProductSubcategory subcategory)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        subcategory.ModifiedDate = DateTime.Now;
        contexto.Entry(subcategory).State = EntityState.Modified;
        return await contexto.SaveChangesAsync() > 0;
    }
}