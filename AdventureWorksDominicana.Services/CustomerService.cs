using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class CustomerService(IDbContextFactory<Contexto> DbFactory) : IService<Customer, int>
{
    public async Task<bool> Guardar(Customer entidad)
    {
        if (!await Existe(entidad.CustomerId))
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
        if (id == 0) return false;
        return await contexto.Customers.AnyAsync(c => c.CustomerId == id);
    }

    private async Task<bool> Insertar(Customer entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        if (entidad.PersonId is not null && entidad.PersonId != 0)
        {
            var person = await contexto.People.FindAsync(entidad.PersonId);
            if (person != null) entidad.Person = person;
        }
        else if (entidad.Person != null)
        {
            contexto.Entry(entidad.Person).State = EntityState.Unchanged;
        }

        if (entidad.StoreId is not null && entidad.StoreId != 0)
        {
            var store = await contexto.Stores.FindAsync(entidad.StoreId);
            if (store != null) entidad.Store = store;
        }
        else if (entidad.Store != null)
        {
            contexto.Entry(entidad.Store).State = EntityState.Unchanged;
        }

        if (entidad.TerritoryId is not null && entidad.TerritoryId != 0)
        {
            var territory = await contexto.SalesTerritories.FindAsync(entidad.TerritoryId);
            if (territory != null) entidad.Territory = territory;
        }
        else if (entidad.Territory != null)
        {
            contexto.Entry(entidad.Territory).State = EntityState.Unchanged;
        }

        if (entidad.Rowguid == Guid.Empty)
        {
            entidad.Rowguid = Guid.NewGuid();
        }

        contexto.Customers.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Customer entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        if (entidad.PersonId is not null && entidad.PersonId != 0)
        {
            var person = await contexto.People.FindAsync(entidad.PersonId);
            if (person != null) entidad.Person = person;
        }
        else if (entidad.Person != null)
        {
            contexto.Entry(entidad.Person).State = EntityState.Unchanged;
        }

        if (entidad.StoreId is not null && entidad.StoreId != 0)
        {
            var store = await contexto.Stores.FindAsync(entidad.StoreId);
            if (store != null) entidad.Store = store;
        }
        else if (entidad.Store != null)
        {
            contexto.Entry(entidad.Store).State = EntityState.Unchanged;
        }

        if (entidad.TerritoryId is not null && entidad.TerritoryId != 0)
        {
            var territory = await contexto.SalesTerritories.FindAsync(entidad.TerritoryId);
            if (territory != null) entidad.Territory = territory;
        }
        else if (entidad.Territory != null)
        {
            contexto.Entry(entidad.Territory).State = EntityState.Unchanged;
        }

        contexto.Customers.Update(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Customer?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
    }

    public async Task<bool> BuscarDuplicado(string nombre, int idExcluido = 0)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return false;
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var existe = await contexto.Customers.AnyAsync(c => c.CustomerId == id);
        if (!existe) return false;

        var tieneOrdenes = await contexto.SalesOrderHeaders.AnyAsync(s => s.CustomerId == id);
        if (tieneOrdenes) return false;

        return await contexto.Customers.Where(c => c.CustomerId == id).ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Customer>> GetList(Expression<Func<Customer, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Customers.Where(criterio).Include(c => c.Person).OrderBy(c => c.CustomerId).ToListAsync();
    }
}
