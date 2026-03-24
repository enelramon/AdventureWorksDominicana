using System.Linq.Expressions;
using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Services;

public class ProductService(IDbContextFactory<Contexto> DbFactory) : IService<Product, int>
{
    public async Task<bool> Guardar(Product entidad)
    {
        entidad.ModifiedDate = DateTime.Now; 

        if (entidad.ProductId == 0 || !await Existe(entidad.ProductId))
        {
            return await Insertar(entidad);
        }
        else
        {
            return await Modificar(entidad);
        }
    }

    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Products.AnyAsync(e => e.ProductId == id);
    }

    private async Task<bool> Insertar(Product entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        if (entidad.Rowguid == Guid.Empty)
        {
            entidad.Rowguid = Guid.NewGuid();
        }

        contexto.Products.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Product entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Products.Update(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Product?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Products.FirstOrDefaultAsync(a => a.ProductId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Products.Where(a => a.ProductId == id).ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Product>> GetList(Expression<Func<Product, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Products.AsNoTracking().Where(criterio).ToListAsync();
    }
}