using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdventureWorksDominicana.Services;

public class CustomerService(IDbContextFactory<Contexto> DbFactory) : IService<Customer, int>
{
    public Task<Customer?> Buscar(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Eliminar(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Customer>> GetList(Expression<Func<Customer, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Customers.Include(p => p.Person).Where(criterio).AsNoTracking().ToListAsync();
    }

    public Task<bool> Guardar(Customer entidad)
    {
        throw new NotImplementedException();
    }
}
