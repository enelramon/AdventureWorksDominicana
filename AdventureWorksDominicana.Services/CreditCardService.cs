using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class CreditCardService(IDbContextFactory<Contexto> DbFactory) : IService<CreditCard, int>
{
    public async Task<bool> Guardar(CreditCard creditCard)
    {
        if (!await Existe(creditCard.CreditCardId))
            return await Insertar(creditCard);
        else
            return await Modificar(creditCard);
    }

    private async Task<bool> Existe(int creditCardId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.CreditCards.AnyAsync(c => c.CreditCardId == creditCardId);
    }

    private async Task<bool> Insertar(CreditCard creditCard)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.CreditCards.Add(creditCard);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(CreditCard creditCard)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(creditCard);
        return await contexto.SaveChangesAsync() > 0;
    }

    public Task<CreditCard?> Buscar(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Eliminar(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<CreditCard>> GetList(Expression<Func<CreditCard, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.CreditCards.Where(criterio).Include(p => p.PersonCreditCards).AsNoTracking().ToListAsync();
    }
}
