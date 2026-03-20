using Aplicada1.Core;
using System;
using System.Collections.Generic;
using System.Text;
using AdventureWorksDominicana.Data.Models;
using Microsoft.EntityFrameworkCore;
using AdventureWorksDominicana.Data.Context;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services
{
    public class CountryRegionsService(IDbContextFactory<Contexto> DbFactory) : IService<CountryRegion, string>
    {
        public async Task<bool> Guardar(CountryRegion entidad)
        {
            if (!await Existe(entidad.CountryRegionCode))
            {
                return await Insertar(entidad);
            }
            else
            {
                return await Modificar(entidad);
            }
        }

        private async Task<bool> Existe(string countryRegionCode)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.CountryRegions.AnyAsync(c => c.CountryRegionCode.Equals(countryRegionCode));
        }

        private async Task<bool> Insertar(CountryRegion entidad)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.CountryRegions.Add(entidad);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(CountryRegion entidad)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.CountryRegions.Update(entidad);
            return await contexto.SaveChangesAsync() > 0;
        }
        public async Task<CountryRegion?> Buscar(string id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.CountryRegions.FirstOrDefaultAsync(c => c.CountryRegionCode.Equals(id));
        }

        public async Task<bool> Eliminar(string id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var country = await Buscar(id);

            if (country == null) return false;

            contexto.CountryRegions.Remove(country);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<List<CountryRegion>> GetList(Expression<Func<CountryRegion, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.CountryRegions.Where(criterio).AsNoTracking().ToListAsync();
        }
    }
}
