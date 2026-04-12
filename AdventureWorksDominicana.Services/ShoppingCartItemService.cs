using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace AdventureWorksDominicana.Services;

public class ShoppingCartItemService(IDbContextFactory<Contexto> DbFactory) : IService<ShoppingCartItem, int>
{

    public async Task<bool> Guardar(ShoppingCartItem CartItem)
    {
        if (!await Existe(CartItem.ShoppingCartItemId))
            return await Insertar(CartItem);
        else
            return await Modificar(CartItem);
    }

    public async Task<bool> Insertar(ShoppingCartItem cartItem)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        bool existeParaEsteUsuario = await contexto.ShoppingCartItems
        .AnyAsync(p => p.ProductId == cartItem.ProductId
                    && p.ShoppingCartId == cartItem.ShoppingCartId);
        if (existeParaEsteUsuario)
        {
            throw new InvalidOperationException("Ya tienes este producto en tu carrito.");
        }
        contexto.ShoppingCartItems.Add(cartItem);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int idCartItem)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ShoppingCartItems.AnyAsync(p => p.ShoppingCartItemId == idCartItem);
    }

    public async Task<bool> Modificar(ShoppingCartItem cartItem)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.ShoppingCartItems.Update(cartItem);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<ShoppingCartItem?> Buscar(int idCartItem)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ShoppingCartItems.FirstOrDefaultAsync(p => p.ShoppingCartItemId == idCartItem);
    }

    public async Task<bool> Eliminar(int idCartitem)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ShoppingCartItems.Where(s => s.ShoppingCartItemId == idCartitem).ExecuteDeleteAsync() > 0;
    }

    public async Task<List<ShoppingCartItem>> GetList(Expression<Func<ShoppingCartItem, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ShoppingCartItems.Include(p => p.Product).ThenInclude(p => p.ProductSubcategory).ThenInclude(p => p.ProductCategory)
         .Include(p => p.Product).ThenInclude(s => s.SpecialOfferProducts).ThenInclude(s => s.SpecialOffer)
         .Include(p => p.Product).ThenInclude(i => i.ProductInventories)
         .Include(p => p.Product).ThenInclude(p => p.ProductModel).ThenInclude(d => d.ProductModelProductDescriptionCultures).ThenInclude(d => d.ProductDescription)
         .Include(p => p.Product).ThenInclude(p => p.ProductProductPhotos).ThenInclude(p => p.ProductPhoto)
         .Where(criterio).ToListAsync();
    }
}
