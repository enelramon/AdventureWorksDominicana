using System.Linq.Expressions;
using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Services;

public class VStoreWithContactService
{
    private readonly IDbContextFactory<Contexto> _dbFactory;

    public VStoreWithContactService(IDbContextFactory<Contexto> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<VStoreWithContact>> Listar(Expression<Func<VStoreWithContact, bool>> criterio)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();

        return await contexto.VStoreWithContacts
            .AsNoTracking()
            .Where(criterio)
            .OrderBy(x => x.BusinessEntityId)
            .ToListAsync();
    }
}