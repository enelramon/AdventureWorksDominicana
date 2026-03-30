using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdventureWorksDominicana.Services;

public class SpecialOfferProductService(IDbContextFactory<Contexto> DbFactory) : IService<SpecialOfferProduct, int>
{
    public Task<SpecialOfferProduct?> Buscar(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Eliminar(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<SpecialOfferProduct>> GetList(Expression<Func<SpecialOfferProduct, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.SpecialOfferProducts.Where(criterio).Include(p => p.Product).ThenInclude(i => i.ProductInventories).Include(o => o.SpecialOffer).AsNoTracking().ToListAsync();
    }

    public Task<bool> Guardar(SpecialOfferProduct entidad)
    {
        throw new NotImplementedException();
    }
}
