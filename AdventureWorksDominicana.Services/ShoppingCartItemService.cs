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
        {
            return await Insertar(CartItem);
        }
        else
        {
            return await Modificar(CartItem);
        }
    }

    public async Task<bool> Insertar(ShoppingCartItem cartItem)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        if (string.IsNullOrWhiteSpace(cartItem.ShoppingCartId))
        {
            string id;
            do
            {
                id = $"SC-{RandomNumberGenerator.GetInt32(0, 1_000_000):D6}";
            } while (await contexto.ShoppingCartItems.AnyAsync(s => s.ShoppingCartId == id));
            cartItem.ShoppingCartId = id;
        }
        if (cartItem.Product != null)
        {
            contexto.Attach(cartItem.Product);
            contexto.Entry(cartItem.Product).State = EntityState.Unchanged;
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

        var cartItem = await Buscar(idCartitem);
        if (cartItem == null) return false;

        contexto.ShoppingCartItems.Remove(cartItem);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<ShoppingCartItem>> GetList(Expression<Func<ShoppingCartItem, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ShoppingCartItems.Where(criterio).ToListAsync();
    }
}
