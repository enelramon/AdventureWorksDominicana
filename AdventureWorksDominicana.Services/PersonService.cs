using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdventureWorksDominicana.Services;

public class PersonService(IDbContextFactory<Contexto> DbFactory) : IService<Person, int>
{
    public Task<Person?> Buscar(int id)
    {
        throw new NotImplementedException();
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
}
