using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class ProductModelService(IDbContextFactory<Contexto> DbContextFactory) : IService<ProductModel, int>
{
    public async Task<ProductModel?> Buscar(int id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.ProductModels.FirstOrDefaultAsync(pm => pm.ProductModelId == id);
    }
    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        var filasAfectadas = await contexto.ProductModels.Where(pm => pm.ProductModelId == id).ExecuteDeleteAsync();
        return filasAfectadas > 0;
    }
    public async Task<List<ProductModel>> GetList(Expression<Func<ProductModel, bool>> criterio)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.ProductModels.Where(criterio).AsNoTracking().ToListAsync();
    }
    public async Task<bool> Guardar(ProductModel productModel)
    {
        if (!await Existe(productModel.ProductModelId))
        {
            return await Insertar(productModel);
        }
        else
        {
            return await Modificar(productModel);
        }
    }
    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.ProductModels.AnyAsync(pm => pm.ProductModelId == id);
    }
    public async Task<bool> Insertar(ProductModel productModel)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        productModel.Rowguid = Guid.NewGuid();
        productModel.ModifiedDate = DateTime.Now;
        contexto.ProductModels.Add(productModel);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Modificar(ProductModel productModel)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        productModel.ModifiedDate = DateTime.Now;
        contexto.Entry(productModel).State = EntityState.Modified;
        return await contexto.SaveChangesAsync() > 0;
    }
}