using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Aplicada1.Core;
using AdventureWorksDominicana.Data.Models;
using Microsoft.EntityFrameworkCore;
using AdventureWorksDominicana.Data.Context;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace AdventureWorksDominicana.Services;

public class PhoneNumberTypeService(IDbContextFactory<Contexto> DbFactory) : IService<PhoneNumberType, int>
{
    public async Task<bool> Guardar(PhoneNumberType entidad)
    {
        if (!await Existe(entidad.PhoneNumberTypeId))
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
        return await contexto.PhoneNumberTypes.AnyAsync(p => p.PhoneNumberTypeId == id);
    }
    private async Task<bool> Insertar(PhoneNumberType phoneNumberType)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.PhoneNumberTypes.Add(phoneNumberType);
        return await contexto.SaveChangesAsync() > 0;
    }
    private async Task<bool> Modificar(PhoneNumberType phoneNumberType)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.PhoneNumberTypes.Update(phoneNumberType);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<PhoneNumberType?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.PhoneNumberTypes.FirstOrDefaultAsync(p => p.PhoneNumberTypeId == id);
    }
    public async Task<bool> BuscarDuplicado(string nombre, int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.PhoneNumberTypes.AnyAsync(p => p.Name.ToLower().Equals(nombre.Trim().ToLower()) && p.PhoneNumberTypeId != id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.PhoneNumberTypes.Where(p => p.PhoneNumberTypeId == id).ExecuteDeleteAsync() > 0;
    }

    public async Task<List<PhoneNumberType>> GetList(Expression<Func<PhoneNumberType, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.PhoneNumberTypes.Where(criterio).AsNoTracking().ToListAsync();
    }
}
