using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
namespace AdventureWorksDominicana.Services;

public class ProductDescriptionService(IDbContextFactory<Contexto> DbFactory) : IService<ProductDescription, int>
{
    public async Task<bool> Guardar(ProductDescription description)
    {
        if (!await Existe(description.ProductDescriptionId))
            return await Insertar(description);
        else
            return await Modificar(description);
    }
    private async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ProductDescriptions.AnyAsync(d => d.ProductDescriptionId == id);
    }
    private async Task<bool> Insertar(ProductDescription description)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.ProductDescriptions.Add(
            new ProductDescription
            {
                Description = description.Description,
                Rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Now
            }
        );
        return await contexto.SaveChangesAsync() > 0;
    }
    private async Task<bool> Modificar(ProductDescription description)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        description.ModifiedDate = DateTime.Now;
        contexto.Update(description);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<ProductDescription?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ProductDescriptions.FirstOrDefaultAsync(d => d.ProductDescriptionId == id);
    }
    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ProductDescriptions.AsNoTracking().Where(d => d.ProductDescriptionId == id).ExecuteDeleteAsync() > 0;
    }
    public async Task<List<ProductDescription>> GetList(Expression<Func<ProductDescription, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ProductDescriptions.Where(criterio).AsNoTracking().ToListAsync();
    }
}