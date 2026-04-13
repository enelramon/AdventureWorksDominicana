using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdventureWorksDominicana.Services;

public class ShiftService(IDbContextFactory<Contexto> DbFactory) : IService<Shift, byte>
{
    public async Task<bool> Existe(byte Id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Shifts.AnyAsync(s => s.ShiftId.Equals(Id));
    }
    public async Task<bool> ExisteNombre(byte Id, string nombre)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Shifts.AnyAsync(s => s.Name == nombre && s.ShiftId != Id);
    }
    public async Task<bool> Insertar(Shift shift)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Shifts.Add(shift);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<Shift?> Buscar(byte Id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Shifts.AsNoTracking().FirstOrDefaultAsync(s => s.ShiftId == Id);
    }

    public async Task<bool> Eliminar(byte Id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        bool Usando = await contexto.EmployeeDepartmentHistories.AnyAsync(s => s.ShiftId == Id);

        if (Usando)
        {
            throw new InvalidOperationException("No se puede eliminar el turno porque tiene empreados asignados");
        }

        var filasAfectadas = await contexto.Shifts.Where(s => s.ShiftId == Id).ExecuteDeleteAsync();

        return filasAfectadas > 0;

        
    }

    public async Task<List<Shift>> GetList(Expression<Func<Shift, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Shifts.Where(criterio).AsNoTracking().ToListAsync();
    }

    public async Task<bool> Guardar(Shift shift)
    {
        if(shift.StartTime == shift.EndTime)
        {
            throw new InvalidOperationException("La hora de inicio y la hora de fin no pueden ser iguales");
        }
        if(await ExisteNombre(shift.ShiftId, shift.Name))
        {
            throw new InvalidOperationException($"Ya existe una tanda con el nombre ingresado.");
        }
        if (!await Existe(shift.ShiftId))
        {
            return await Insertar(shift);
        }
        else
        {
            return await Modificar(shift);
        }
    }

    public async Task<bool> Modificar(Shift shift)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        shift.ModifiedDate = DateTime.Now;
        contexto.Shifts.Update(shift);

        return await contexto.SaveChangesAsync() > 0;

    }
}
