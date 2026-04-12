using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdventureWorksDominicana.Services;

public class PersonCreditCardService(IDbContextFactory<Contexto> DbFactory) : IService<PersonCreditCard, int>
{
    public async Task<bool> Guardar(PersonCreditCard personCreditCard)
    {
        if (!await Existe(personCreditCard.BusinessEntityId, personCreditCard.CreditCardId))
            return await Insertar(personCreditCard);
        else
            return await Modificar(personCreditCard);
    }

    private async Task<bool> Existe(int personCreditCardId, int creditCardId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.PersonCreditCards.AnyAsync(p => p.BusinessEntityId == personCreditCardId && p.CreditCardId == creditCardId);
    }

    private async Task<bool> Insertar(PersonCreditCard personCreditCard)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.PersonCreditCards.Add(personCreditCard);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(PersonCreditCard personCreditCard)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(personCreditCard);
        return await contexto.SaveChangesAsync() > 0;
    }
    public Task<PersonCreditCard?> Buscar(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Eliminar(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<PersonCreditCard>> GetList(Expression<Func<PersonCreditCard, bool>> criterio)
    {
        throw new NotImplementedException();
    }

}
