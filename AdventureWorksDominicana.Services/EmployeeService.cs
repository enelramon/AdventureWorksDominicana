using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdventureWorksDominicana.Services;

public class EmployeeService(IDbContextFactory<Contexto> DbFactory) : IService<Employee, int>
{
    public Task<Employee?> Buscar(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Eliminar(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Employee>> GetList(Expression<Func<Employee, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Employees.Where(criterio).AsNoTracking().ToListAsync();
    }

    public Task<bool> Guardar(Employee entidad)
    {
        throw new NotImplementedException();
    }
}
