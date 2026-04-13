using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class SpecialOfferService(IDbContextFactory<Contexto> DbContextFactory) : IService<SpecialOffer, int>
{
    public async Task<SpecialOffer?> Buscar(int id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.SpecialOffers.AsNoTracking().FirstOrDefaultAsync(s => s.SpecialOfferId == id);
    }
    public async Task<List<SpecialOffer>> GetList(Expression<Func<SpecialOffer, bool>> criterio)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.SpecialOffers.AsNoTracking().Where(criterio).ToListAsync();
    }
    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.SpecialOffers.AnyAsync(s => s.SpecialOfferId == id);
    }
    public async Task<bool> Guardar(SpecialOffer offer)
    {
        if (!await Existe(offer.SpecialOfferId)) return await Insertar(offer);
        return await Modificar(offer);
    }
    public async Task<bool> Insertar(SpecialOffer offer)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        offer.ModifiedDate = DateTime.Now;
        offer.Rowguid = Guid.NewGuid();
        contexto.SpecialOffers.Add(offer);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Modificar(SpecialOffer offer)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        offer.ModifiedDate = DateTime.Now;
        contexto.SpecialOffers.Update(offer);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbContextFactory.CreateDbContextAsync();
        return await contexto.SpecialOffers.Where(s => s.SpecialOfferId == id).ExecuteDeleteAsync() > 0;
    }
}
