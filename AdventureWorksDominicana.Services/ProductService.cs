<<<<<<< issue/40_crear_funcionalidad_billofmaterial
﻿using System.Linq.Expressions;
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
=======
﻿using AdventureWorksDominicana.Data.Context;
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
>>>>>>> master
