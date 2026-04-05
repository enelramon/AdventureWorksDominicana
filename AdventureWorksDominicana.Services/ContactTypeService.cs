using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdventureWorksDominicana.Services;

public class ContactTypeService(IDbContextFactory<Contexto> DbFactory) : IService<ContactType, int>
{
    public async Task<bool> Guardar(ContactType entidad)
    {
        if (!await Existe(entidad.ContactTypeId))
        {
            return await Insertar(entidad);
        }
        else
        {
            return await Modificar(entidad);
        }
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ContactTypes.AnyAsync(c => c.ContactTypeId == id);
    }

    private async Task<bool> Insertar(ContactType entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.ContactTypes.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(ContactType entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.ContactTypes.Update(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<ContactType?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ContactTypes.FirstOrDefaultAsync(c => c.ContactTypeId == id);
    }

    public async Task<bool> BuscarDuplicado(string nombre, int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ContactTypes.AnyAsync(c => c.Name.ToLower().Equals(nombre.Trim().ToLower()) && c.ContactTypeId != id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var existe = await contexto.ContactTypes.AnyAsync(c => c.ContactTypeId == id);
        if (!existe)
        {
            throw new InvalidOperationException("No se puede eliminar: el tipo de contacto no existe");
        }

        var tieneContacts = await contexto.BusinessEntityContacts.AnyAsync(p => p.ContactTypeId == id);
        if (tieneContacts)
        {
            throw new InvalidOperationException("No se puede eliminar: el tipo de contacto tiene contactos asignados");
        }

        return await contexto.ContactTypes.Where(c => c.ContactTypeId == id).ExecuteDeleteAsync() > 0;
    }

    public async Task<List<ContactType>> GetList(Expression<Func<ContactType, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ContactTypes.Where(criterio).AsNoTracking().ToListAsync();
    }
}
