using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class ProductInventoryService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(ProductInventory inventory)
    {
        inventory.Product = null;
        inventory.Location = null;

        if (!await Existe(inventory.ProductId, inventory.LocationId))
            return await Insertar(inventory);
        else
            return await Modificar(inventory);
    }

    private async Task<bool> Existe(int productId, short locationId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ProductInventories
            .AnyAsync(pi => pi.ProductId == productId && pi.LocationId == locationId);
    }

    private async Task<bool> Insertar(ProductInventory inventory)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        using var transaction = await contexto.Database.BeginTransactionAsync();
        try
        {
            inventory.Rowguid = Guid.NewGuid();
            inventory.ModifiedDate = DateTime.Now;
            inventory.Shelf = string.IsNullOrWhiteSpace(inventory.Shelf) ? "A" : inventory.Shelf;
            inventory.Bin = inventory.Bin <= 0 ? (byte)1 : inventory.Bin;

            contexto.ProductInventories.Add(inventory);
            contexto.TransactionHistories.Add(new TransactionHistory
            {
                ProductId = inventory.ProductId,
                ReferenceOrderId = 0,
                TransactionDate = DateTime.Now,
                TransactionType = "W",
                Quantity = inventory.Quantity,
                ActualCost = 0,
                ModifiedDate = DateTime.Now
            });

            await contexto.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al Insertar: {ex.Message}");
            await transaction.RollbackAsync();
            return false;
        }
    }

    private async Task<bool> Modificar(ProductInventory inventory)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        using var transaction = await contexto.Database.BeginTransactionAsync();
        try
        {
            var original = await contexto.ProductInventories
                .FirstOrDefaultAsync(pi => pi.ProductId == inventory.ProductId && pi.LocationId == inventory.LocationId);

            if (original == null) return false;

            int diferencia = inventory.Quantity - original.Quantity;
            if (diferencia != 0)
            {
                contexto.TransactionHistories.Add(new TransactionHistory
                {
                    ProductId = inventory.ProductId,
                    ReferenceOrderId = 0,
                    TransactionDate = DateTime.Now,
                    TransactionType = "W",
                    Quantity = Math.Abs(diferencia),
                    ActualCost = 0,
                    ModifiedDate = DateTime.Now
                });
            }

            original.Quantity = inventory.Quantity;
            original.Shelf = inventory.Shelf;
            original.Bin = inventory.Bin;
            original.ModifiedDate = DateTime.Now;

            await contexto.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al Modificar: {ex.Message}");
            await transaction.RollbackAsync();
            return false;
        }
    }

    public async Task<bool> AjustarExistencia(int productId, short locationId, short cantidadCambio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        using var transaction = await contexto.Database.BeginTransactionAsync();
        try
        {
            var item = await contexto.ProductInventories
                .FirstOrDefaultAsync(pi => pi.ProductId == productId && pi.LocationId == locationId);
            if (item == null) return false;

            item.Quantity = (short)(item.Quantity + cantidadCambio);
            item.ModifiedDate = DateTime.Now;

            contexto.TransactionHistories.Add(new TransactionHistory
            {
                ProductId = productId,
                ReferenceOrderId = 0,
                TransactionDate = DateTime.Now,
                TransactionType = "W",
                Quantity = Math.Abs(cantidadCambio),
                ActualCost = 0,
                ModifiedDate = DateTime.Now
            });

            await contexto.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return false;
        }
    }

    public async Task<List<ProductInventory>> GetList(Expression<Func<ProductInventory, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ProductInventories
            .Include(pi => pi.Product).ThenInclude(p => p.ProductSubcategory).ThenInclude(p => p.ProductCategory)
            .Include(pi => pi.Product).ThenInclude(p => p.ProductModel).ThenInclude(d => d.ProductModelProductDescriptionCultures).ThenInclude(d => d.ProductDescription)
            .Include(pi => pi.Product).ThenInclude(p => p.SizeUnitMeasureCodeNavigation)
            .Include(pi => pi.Product).ThenInclude(p => p.WeightUnitMeasureCodeNavigation)
            .Include(pi => pi.Product).ThenInclude(p => p.ProductProductPhotos).ThenInclude(ppp => ppp.ProductPhoto)
            .Include(pi => pi.Location)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ProductInventory?> Buscar(int productId, short locationId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ProductInventories
            .Include(pi => pi.Product)
            .Include(pi => pi.Location)
            .AsNoTracking()
            .FirstOrDefaultAsync(pi => pi.ProductId == productId && pi.LocationId == locationId);
    }
}