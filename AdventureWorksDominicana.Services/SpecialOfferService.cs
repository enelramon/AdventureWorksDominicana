using System.Linq.Expressions;
using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Services;

public class SpecialOfferService
{
    private readonly IDbContextFactory<Contexto> _dbFactory;

    public SpecialOfferService(IDbContextFactory<Contexto> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<bool> Existe(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.SpecialOffers.AnyAsync(s => s.SpecialOfferId == id);
    }

    public async Task<bool> Guardar(SpecialOffer specialOffer)
    {
        if (!await Existe(specialOffer.SpecialOfferId))
            return await Insertar(specialOffer);

        return await Modificar(specialOffer);
    }

    private async Task<bool> Insertar(SpecialOffer specialOffer)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();

        contexto.SpecialOffers.Add(specialOffer);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(SpecialOffer specialOffer)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();

        contexto.SpecialOffers.Update(specialOffer);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<SpecialOffer?> Buscar(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();

        return await contexto.SpecialOffers
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.SpecialOfferId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();

        var specialOffer = await contexto.SpecialOffers
            .FirstOrDefaultAsync(s => s.SpecialOfferId == id);

        if (specialOffer is null)
            return false;

        contexto.SpecialOffers.Remove(specialOffer);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<SpecialOffer>> Listar(Expression<Func<SpecialOffer, bool>> criterio)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();

        return await contexto.SpecialOffers
            .AsNoTracking()
            .Where(criterio)
            .OrderBy(s => s.SpecialOfferId)
            .ToListAsync();
    }
}