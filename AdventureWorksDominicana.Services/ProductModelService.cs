using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class ProductModelService(IDbContextFactory<Contexto> DbFactory) : IService<ProductModel, int>
{
    public async Task<bool> Existe(int Id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ProductModels.AnyAsync(p => p.ProductModelId == Id);
    }

    public async Task<bool> Insertar(ProductModel productModel)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        productModel.Rowguid = Guid.NewGuid();
        productModel.ModifiedDate = DateTime.Now;
        contexto.ProductModels.Add(productModel);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<ProductModel?> Buscar(int Id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ProductModels.AsNoTracking().FirstOrDefaultAsync(p => p.ProductModelId == Id);
    }

    public async Task<bool> Eliminar(int Id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        bool Usando = await contexto.Products.AnyAsync(p => p.ProductModelId == Id);

        if (Usando)
        {
            throw new InvalidOperationException("No se puede eliminar el modelo de producto porque tiene productos asociados");
        }

        var filasAfectadas = await contexto.ProductModels.Where(p => p.ProductModelId == Id).ExecuteDeleteAsync();

        return filasAfectadas > 0;
    }

    public async Task<List<ProductModel>> GetList(Expression<Func<ProductModel, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
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

    public async Task<bool> Modificar(ProductModel productModel)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        productModel.ModifiedDate = DateTime.Now;
        contexto.ProductModels.Update(productModel);

        return await contexto.SaveChangesAsync() > 0;
    }
}

