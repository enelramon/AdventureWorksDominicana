using System.Linq.Expressions;
using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Services;

public class EmailAddressService
{
    private readonly IDbContextFactory<Contexto> _dbFactory;

    public EmailAddressService(IDbContextFactory<Contexto> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<EmailAddress>> Listar(Expression<Func<EmailAddress, bool>> criterio)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();

        return await contexto.EmailAddresses
            .AsNoTracking()
            .Where(criterio)
            .OrderBy(e => e.BusinessEntityId)
            .ThenBy(e => e.EmailAddressId)
            .ToListAsync();
    }

    public async Task<EmailAddress?> Buscar(int businessEntityId, int emailAddressId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();

        return await contexto.EmailAddresses
            .AsNoTracking()
            .FirstOrDefaultAsync(e =>
                e.BusinessEntityId == businessEntityId &&
                e.EmailAddressId == emailAddressId);
    }

    public async Task<bool> Guardar(EmailAddress entity)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();

        entity.BusinessEntity = null!;

        var existe = await contexto.EmailAddresses.AnyAsync(e =>
            e.BusinessEntityId == entity.BusinessEntityId &&
            e.EmailAddressId == entity.EmailAddressId);

        if (!existe)
            contexto.EmailAddresses.Add(entity);
        else
            contexto.EmailAddresses.Update(entity);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int businessEntityId, int emailAddressId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();

        var entity = await contexto.EmailAddresses.FirstOrDefaultAsync(e =>
            e.BusinessEntityId == businessEntityId &&
            e.EmailAddressId == emailAddressId);

        if (entity is null)
            return false;

        contexto.EmailAddresses.Remove(entity);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Person>> GetPersonas()
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();

        return await contexto.People
            .AsNoTracking()
            .OrderBy(p => p.FirstName)
            .ThenBy(p => p.LastName)
            .ToListAsync();
    }
}