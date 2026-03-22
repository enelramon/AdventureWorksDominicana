using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class AddressTypeService(IDbContextFactory<Contexto> DbFactory) : IService<AddressType, int>
{
    public async Task<List<AddressType>> Listar(Expression<Func<AddressType, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.AddressTypes.Where(criterio).ToListAsync();
    }

    public async Task<bool> Guardar(AddressType addressType)
    {
        if (!await Existe(addressType.AddressTypeId))
            return await Insertar(addressType);
        else
            return await Modificar(addressType);
    }

    public async Task<bool> Insertar(AddressType addressType)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        addressType.Rowguid = Guid.NewGuid();
        addressType.ModifiedDate = DateTime.Now;
        contexto.AddressTypes.Add(addressType);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.AddressTypes.AnyAsync(a => a.AddressTypeId == id);
    }

    public async Task<bool> Modificar(AddressType addressType)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        addressType.ModifiedDate = DateTime.Now;
        contexto.AddressTypes.Update(addressType);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<AddressType?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.AddressTypes.FirstOrDefaultAsync(a => a.AddressTypeId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var addressType = await Buscar(id);
        if (addressType == null) return false;
        contexto.AddressTypes.Remove(addressType);
        return await contexto.SaveChangesAsync() > 0;
    }

    public Task<List<AddressType>> GetList(Expression<Func<AddressType, bool>> criterio)
    {
        throw new NotImplementedException();
    }
}