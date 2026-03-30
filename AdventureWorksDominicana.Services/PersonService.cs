using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class PersonService(IDbContextFactory<Contexto> DbFactory) : IService<Person, int>
{
    public async Task<Person?> Buscar(int id)
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        return await context.People.FirstOrDefaultAsync(p => p.BusinessEntityId == id);
    }

    public Task<bool> Eliminar(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Person>> GetList(Expression<Func<Person, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.People.Where(criterio).AsNoTracking().ToListAsync();
    }

    public Task<bool> Guardar(Person entidad)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Person>> BuscarPorNombre(string nombre)
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        return await context.People.Where(p => p.FirstName.Contains(nombre) || p.LastName.Contains(nombre)).Take(10).ToListAsync();
    }
}
