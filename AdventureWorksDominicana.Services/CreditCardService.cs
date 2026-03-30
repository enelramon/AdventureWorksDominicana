using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class CreditCardService(IDbContextFactory<Contexto> DbFactory) : IService<CreditCard, int>
{
    public Task<bool> Guardar(CreditCard entidad)
    {
        throw new NotImplementedException();
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
        return await contexto.CreditCards.Where(criterio).AsNoTracking().ToListAsync();
    }
}
