using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AdventureWorksDominicana.Services;

public class PersonService(IDbContextFactory<Contexto> DbFactory) : IService<Person, int>
{
    public async Task<Person?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.People
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.BusinessEntityId == id);
    }

    public async Task<bool> Guardar(Person entidad)
    {
        if (!await Existe(entidad.BusinessEntityId))
            return await Insertar(entidad);
        else
            return await Modificar(entidad);
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.People
            .AnyAsync(p => p.BusinessEntityId == id);
    }

    private async Task<bool> Insertar(Person entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        if (entidad.Rowguid == Guid.Empty)
            entidad.Rowguid = Guid.NewGuid();

        entidad.ModifiedDate = DateTime.Now;

        entidad.BusinessEntity = new BusinessEntity
        {
            Rowguid = Guid.NewGuid(),
            ModifiedDate = DateTime.Now
        };

        contexto.People.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Person entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        entidad.ModifiedDate = DateTime.Now;

        contexto.People.Update(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var eliminados = await contexto.People
            .Where(p => p.BusinessEntityId == id)
            .ExecuteDeleteAsync();

        return eliminados > 0;
    }

    public async Task<List<Person>> GetList(Expression<Func<Person, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.People
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Person>> BuscarPorNombre(string busqueda)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        if (string.IsNullOrWhiteSpace(busqueda))
            return new List<Person>();

        busqueda = busqueda.Trim().ToLower();

        return await contexto.People
            .AsNoTracking()
            .Where(p =>
                (p.FirstName != null && p.FirstName.ToLower().Contains(busqueda)) ||
                (p.MiddleName != null && p.MiddleName.ToLower().Contains(busqueda)) ||
                (p.LastName != null && p.LastName.ToLower().Contains(busqueda)))
            .OrderBy(p => p.FirstName)
            .ThenBy(p => p.LastName)
            .ToListAsync();
    }
}