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
        return await contexto.ProductModels
            .Include(pm => pm.ProductModelProductDescriptionCultures)
                .ThenInclude(p => p.ProductDescription)
            .Include(pm => pm.ProductModelProductDescriptionCultures)
                .ThenInclude(p => p.Culture)
            .FirstOrDefaultAsync(pm => pm.ProductModelId == id);
    }
    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        try
        {
            return await contexto.ProductModels.Where(pm => pm.ProductModelId == id).ExecuteDeleteAsync() > 0;
        }
        catch (DbUpdateException ex)
        {
            throw new ProductDependentDataException("No se puede eliminar el modelo porque tiene productos asociados.", ex);
        }
        catch { return false; }
    }
    public async Task<List<ProductModel>> GetList(Expression<Func<ProductModel, bool>> criterio)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.ProductModels
            .Include(pm => pm.ProductModelProductDescriptionCultures) // Cargamos los vínculos para el conteo
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
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

        if (productModel.ProductModelProductDescriptionCultures != null)
        {
            foreach (var item in productModel.ProductModelProductDescriptionCultures)
            {
                item.ProductDescription = null;
            }
        }

        contexto.ProductModels.Add(productModel);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(ProductModel productModel)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        productModel.ModifiedDate = DateTime.Now;

        // 1. Borramos los registros viejos en la tabla intermedia
        var antiguos = await contexto.ProductModelProductDescriptionCultures
            .Where(x => x.ProductModelId == productModel.ProductModelId).ToListAsync();
        contexto.ProductModelProductDescriptionCultures.RemoveRange(antiguos);

        // 2. Insertamos manualmente los seleccionados en la vista
        if (productModel.ProductModelProductDescriptionCultures != null)
        {
            foreach (var item in productModel.ProductModelProductDescriptionCultures)
            {
                contexto.ProductModelProductDescriptionCultures.Add(new ProductModelProductDescriptionCulture
                {
                    ProductModelId = productModel.ProductModelId,
                    ProductDescriptionId = item.ProductDescriptionId,
                    CultureId = item.CultureId,
                    ModifiedDate = DateTime.Now
                });
            }
            productModel.ProductModelProductDescriptionCultures = null!;
        }

        // 4. Marcamos la entidad madre como editada
        contexto.Entry(productModel).State = EntityState.Modified;

        return await contexto.SaveChangesAsync() > 0;
    }

}