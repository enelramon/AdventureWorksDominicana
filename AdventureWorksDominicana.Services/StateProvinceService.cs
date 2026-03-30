using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdventureWorksDominicana.Services;

public class StateProvinceService(IDbContextFactory<Contexto> DbFactory) : IService<StateProvince, int>
{
    public async Task<bool> Guardar(StateProvince entidad)
    {
        if (!await Existe(entidad.StateProvinceId))
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
        return await contexto.StateProvinces.AnyAsync(s => s.StateProvinceId == id);
    }

    private async Task<bool> Insertar(StateProvince entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.StateProvinces.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }
    private async Task<bool> Modificar(StateProvince entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.StateProvinces.Update(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<StateProvince?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.StateProvinces.FirstOrDefaultAsync(s => s.StateProvinceId == id);
    }

    public async Task<bool> BuscarRepetido(string codigo, int id, string codigoPais)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.StateProvinces.AnyAsync(s => s.StateProvinceCode.ToLower().Equals(codigo) && s.StateProvinceId != id && s.CountryRegionCode.ToLower().Equals(codigoPais));
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.StateProvinces.Where(s => s.StateProvinceId == id).ExecuteDeleteAsync() > 0;
    }

    public async Task<List<StateProvince>> GetList(Expression<Func<StateProvince, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.StateProvinces.Where(criterio).ToListAsync();
    }
}
