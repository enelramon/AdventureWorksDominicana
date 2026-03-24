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
    public class CultureService(IDbContextFactory<Contexto> DbFactory) : IService<Culture, string>
    {
        public async Task<bool> Guardar(Culture culture)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var existe = await contexto.Cultures.AnyAsync(c => c.CultureId == culture.CultureId);

            if (!existe)
            {
                return await Insertar(culture);
            }
            else
            {
                return await Modificar(culture);
            }
        }

        private async Task<bool> Insertar(Culture culture)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            culture.ModifiedDate = DateTime.Now;
            contexto.Cultures.Add(culture);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Culture culture)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            culture.ModifiedDate = DateTime.Now;
            contexto.Cultures.Update(culture);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Culture?> Buscar(string id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Cultures
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CultureId == id);
        }

        public async Task<bool> Eliminar(string id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var culture = await contexto.Cultures.FindAsync(id);

            if (culture == null)
            {
                return false;
            }

            contexto.Cultures.Remove(culture);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<List<Culture>> GetList(Expression<Func<Culture, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Cultures
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}