using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class PayrollParameterService(IDbContextFactory<Contexto> DbFactory) : IService<PayrollParameter, int>
{
    public async Task<bool> Guardar(PayrollParameter entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        //  Solo puede haber una ley activa
        if (entidad.IsActive)
        {
            var otrosActivos = await contexto.PayrollParameters
                .Where(p => p.PayrollParameterId != entidad.PayrollParameterId && p.IsActive)
                .ToListAsync();

            foreach (var p in otrosActivos) p.IsActive = false;
        }

        if (entidad.PayrollParameterId == 0)
            contexto.PayrollParameters.Add(entidad);
        else
            contexto.PayrollParameters.Update(entidad);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<PayrollParameter>> GetList(Expression<Func<PayrollParameter, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.PayrollParameters.Where(criterio).ToListAsync();
    }

    public async Task<PayrollParameter?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.PayrollParameters.FindAsync(id);
    }
    public async Task<bool> Eliminar(int id) 
    { 
        throw new NotImplementedException();
    }

    public async Task<PayrollParameter?> ObtenerActive()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.PayrollParameters.AsNoTracking().Where(e => e.IsActive == true).FirstOrDefaultAsync();
    }
}