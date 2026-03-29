using System.Linq.Expressions;
using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Services;

public class BillOfMaterialService(IDbContextFactory<Contexto> DbFactory) : IService<BillOfMaterial, int>
{
    public async Task<bool> Guardar(BillOfMaterial entidad)
    {
        entidad.ModifiedDate = DateTime.Now;

        if (entidad.BillOfMaterialsId == 0 || !await Existe(entidad.BillOfMaterialsId))
        {
            return await Insertar(entidad);
        }
        else
        {
            return await Modificar(entidad);
        }
    }

    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.BillOfMaterials.AnyAsync(e => e.BillOfMaterialsId == id);
    }

    private async Task<bool> Insertar(BillOfMaterial entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.BillOfMaterials.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(BillOfMaterial entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.BillOfMaterials.Update(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<BillOfMaterial?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.BillOfMaterials.FirstOrDefaultAsync(a => a.BillOfMaterialsId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.BillOfMaterials.Where(a => a.BillOfMaterialsId == id).ExecuteDeleteAsync() > 0;
    }

    public async Task<List<BillOfMaterial>> GetList(Expression<Func<BillOfMaterial, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.BillOfMaterials.AsNoTracking().Where(criterio).ToListAsync();
    }
}