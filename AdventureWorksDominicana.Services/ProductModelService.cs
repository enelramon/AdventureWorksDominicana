using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class ProductModelService(IDbContextFactory<Contexto> DbFactory) : IService<ProductModel, int>
{
    public async Task<List<ProductModel>> Listar(Expression<Func<ProductModel, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ProductModels.Where(criterio).AsNoTracking().ToListAsync();
    }

    public async Task<bool> Guardar(ProductModel productModel)
    {
        if (!await Existe(productModel.ProductModelId))
            return await Insertar(productModel);
        else
            return await Modificar(productModel);
    }

    public async Task<bool> Insertar(ProductModel productModel)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        productModel.Rowguid = Guid.NewGuid();
        productModel.ModifiedDate = DateTime.Now;
        contexto.ProductModels.Add(productModel);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ProductModels.AnyAsync(a => a.ProductModelId == id);
    }

    public async Task<bool> Modificar(ProductModel productModel)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        productModel.ModifiedDate = DateTime.Now;
        contexto.ProductModels.Update(productModel);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<ProductModel?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ProductModels.AsNoTracking().FirstOrDefaultAsync(a => a.ProductModelId == id);
    }

    public async Task<bool> BuscarDuplicado(string nombre, int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ProductModels
            .AnyAsync(a => a.Name.ToLower() == nombre.ToLower() && a.ProductModelId != id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        bool usando = await contexto.Products.AnyAsync(p => p.ProductModelId == id);
        if (usando)
        {
            throw new InvalidOperationException("No se puede eliminar el modelo de producto porque tiene productos asociados");
        }

        var filasAfectadas = await contexto.ProductModels.Where(p => p.ProductModelId == id).ExecuteDeleteAsync();
        return filasAfectadas > 0;
    }

    public async Task<List<ProductModel>> GetList(Expression<Func<ProductModel, bool>> criterio)
    {
        return await Listar(criterio);
    }
}