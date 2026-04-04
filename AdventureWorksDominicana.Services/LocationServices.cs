using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Services
{
    public class LocationService(IDbContextFactory<Contexto> DbFactory) : IService<Location, short>
    {
        public async Task<bool> Guardar(Location location)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var existe = await contexto.Locations.AnyAsync(l => l.LocationId == location.LocationId);

            if (!existe)
            {
                return await Insertar(location);
            }
            else
            {
                return await Modificar(location);
            }
        }

        private async Task<bool> Insertar(Location location)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            location.ModifiedDate = DateTime.Now;
            contexto.Locations.Add(location);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Location location)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            location.ModifiedDate = DateTime.Now;
            contexto.Locations.Update(location);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Location?> Buscar(short id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Locations
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.LocationId == id);
        }

        public async Task<bool> Eliminar(short id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var location = await contexto.Locations.FindAsync(id);

            if (location == null)
            {
                return false;
            }

            contexto.Locations.Remove(location);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<List<Location>> GetList(Expression<Func<Location, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Locations
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}