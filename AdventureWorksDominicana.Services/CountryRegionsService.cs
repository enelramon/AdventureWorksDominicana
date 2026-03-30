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
            return await contexto.CountryRegions
                .Include(c => c.SalesTerritories)
                .Include(c => c.StateProvinces)
                .Include(c => c.CountryRegionCurrencies)
                .FirstOrDefaultAsync(c => c.CountryRegionCode.Equals(id));
        }

        public async Task<CountryRegion?> BuscarDuplicado(string id, string idExcluido)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.CountryRegions.FirstOrDefaultAsync(c => c.CountryRegionCode.Equals(id) && !c.CountryRegionCode.Equals(idExcluido));
        }

        public async Task<bool> Eliminar(string id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();

            var tieneEstados = await contexto.StateProvinces.AnyAsync(s => s.CountryRegionCode == id);
            if (tieneEstados)
                throw new InvalidOperationException("No se puede eliminar: tiene estados/provincias asociados");

            var tieneTerritorios = await contexto.SalesTerritories.AnyAsync(t => t.CountryRegionCode == id);
            if (tieneTerritorios)
                throw new InvalidOperationException("No se puede eliminar: tiene territorios de ventas asociados");

            var tieneMonedas = await contexto.CountryRegionCurrencies.AnyAsync(c => c.CountryRegionCode == id);
            if (tieneMonedas)
                throw new InvalidOperationException("No se puede eliminar: tiene monedas asociadas");

            var country = await contexto.CountryRegions.FirstOrDefaultAsync(c => c.CountryRegionCode == id);
            if (country == null)
                throw new InvalidOperationException("El país/región no existe");

            return await contexto.CountryRegions.Where(c => c.CountryRegionCode.Equals(id)).ExecuteDeleteAsync() > 0;
        }

        public async Task<List<CountryRegion>> GetList(Expression<Func<CountryRegion, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.CountryRegions.Where(criterio).AsNoTracking().ToListAsync();
        }
    }
}
