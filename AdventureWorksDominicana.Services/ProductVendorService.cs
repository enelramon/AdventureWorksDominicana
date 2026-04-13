using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AdventureWorksDominicana.Services;

public class ProductVendorService(IDbContextFactory<Contexto> DbFactory)
{
    // =======================================================
    // MÉTODOS DE LECTURA (CONSULTAS Y LISTAS)
    // =======================================================

    public async Task<List<ProductVendor>> GetList(Expression<Func<ProductVendor, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ProductVendors
            .Include(p => p.Product)
            .Include(p => p.BusinessEntity) // Para acceder a los datos del proveedor
            .Where(criterio).ToListAsync();
    }

    public async Task<IEnumerable<ProductVendor>> GetAllAsync()
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        return await _context.ProductVendors.ToListAsync();
    }

    // Método seguro para buscar un registro por su llave compuesta (Ideal para la vista Edit)
    public async Task<ProductVendor?> BuscarAsync(int productId, int businessEntityId)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        return await _context.ProductVendors.FindAsync(productId, businessEntityId);
    }

    // --- Listas para los Menús Desplegables (Dropdowns) ---

    public async Task<List<UnitMeasure>> GetUnidadesDeMedidaAsync()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.UnitMeasures.OrderBy(u => u.Name).ToListAsync();
    }

    public async Task<List<Product>> GetProductosAsync()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        // Ordenamos por nombre para que el usuario lo encuentre fácil
        return await contexto.Products.OrderBy(p => p.Name).ToListAsync();
    }

    public async Task<List<Vendor>> GetProveedoresAsync()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Vendors.OrderBy(v => v.Name).ToListAsync();
    }

    // =======================================================
    // MÉTODOS DE ESCRITURA (GUARDAR, CREAR, EDITAR, ELIMINAR)
    // =======================================================

    public async Task<bool> Guardar(ProductVendor entidad)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        var exists = await _context.ProductVendors
            .AnyAsync(pv => pv.ProductId == entidad.ProductId && pv.BusinessEntityId == entidad.BusinessEntityId);

        if (exists)
        {
            await EditAsync(entidad);
        }
        else
        {
            await CreateAsync(entidad);
        }

        return true;
    }

    public async Task<ProductVendor> CreateAsync(ProductVendor productVendor)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();

        // Actualizamos la fecha de modificación
        productVendor.ModifiedDate = DateTime.Now;

        try
        {
            await _context.ProductVendors.AddAsync(productVendor);
            await _context.SaveChangesAsync();
            return productVendor;
        }
        catch (DbUpdateException ex)
        {
            // Extrae el mensaje real si falla una llave foránea u otra restricción de SQL
            var mensajeReal = ex.InnerException?.Message ?? ex.Message;
            throw new Exception($"Error de base de datos al crear: {mensajeReal}");
        }
    }

    public async Task<ProductVendor> EditAsync(ProductVendor productVendor)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();

        var existing = await _context.ProductVendors
            .FindAsync(productVendor.ProductId, productVendor.BusinessEntityId);

        if (existing == null)
        {
            throw new Exception("El registro que intenta modificar no existe.");
        }

        _context.Entry(existing).CurrentValues.SetValues(productVendor);
        existing.ModifiedDate = DateTime.Now;

        try
        {
            await _context.SaveChangesAsync();
            return existing;
        }
        catch (DbUpdateException ex)
        {
            var mensajeReal = ex.InnerException?.Message ?? ex.Message;
            throw new Exception($"Error de base de datos al actualizar: {mensajeReal}");
        }
    }

    public async Task<bool> DeleteAsync(int productId, int businessEntityId)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();

        var entity = await _context.ProductVendors.FindAsync(productId, businessEntityId);

        if (entity != null)
        {
            _context.ProductVendors.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}