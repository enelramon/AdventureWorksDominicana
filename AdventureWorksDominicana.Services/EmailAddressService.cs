using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdventureWorksDominicana.Services;

public class EmailAddressService(IDbContextFactory<Contexto> DbFactory) : IService<EmailAddress, int>
{
    public async Task<EmailAddress?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EmailAddresses.Include(b => b.BusinessEntity).ThenInclude(c => c.Customers).FirstOrDefaultAsync(e => e.EmailAddressId == id);
    }

    public async Task<EmailAddress?> BuscarPorEmail(string email)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        Console.WriteLine("\n" + email + "\n");
        return await contexto.EmailAddresses.FirstOrDefaultAsync(e => e.EmailAddress1.Trim().Equals(email));
    }

    public Task<bool> Eliminar(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<EmailAddress>> GetList(Expression<Func<EmailAddress, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EmailAddresses.Where(criterio).AsNoTracking().ToListAsync();
    }

    public Task<bool> Guardar(EmailAddress entidad)
    {
        throw new NotImplementedException();
    }
}
