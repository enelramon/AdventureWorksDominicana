using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Xml;

namespace AdventureWorksDominicana.Services;

public class StoreService(IDbContextFactory<Contexto> DbFactory) : IService<Store, int>
{
    public async Task<bool> Guardar(Store entidad)
    {
        if (!await Existe(entidad.BusinessEntityId))
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
        return await contexto.Stores.AnyAsync(s => s.BusinessEntityId == id);
    }

    private async Task<bool> Insertar(Store entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        if (entidad.SalesPerson != null)
        {
            contexto.Entry(entidad.SalesPerson).State = EntityState.Unchanged;
        }
        if (entidad.BusinessEntity == null && entidad.BusinessEntityId == 0)
        {
            var newBe = new BusinessEntity
            {
                Rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.UtcNow
            };

            contexto.BusinessEntities.Add(newBe);
            await contexto.SaveChangesAsync();

            entidad.BusinessEntityId = newBe.BusinessEntityId;
            entidad.BusinessEntity = newBe;
        }
        if (string.IsNullOrWhiteSpace(entidad.Demographics))
        {
            entidad.Demographics = null; 
        }
        else
        {
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(entidad.Demographics);
                entidad.Demographics = doc.OuterXml;
            }
            catch (XmlException)
            {
                throw new ArgumentException("Demographics must be well-formed XML with a single root element.");
            }
        }
        contexto.Stores.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Store entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        if (entidad.SalesPerson != null)
        {
            contexto.Entry(entidad.SalesPerson).State = EntityState.Unchanged;
        }
        if (entidad.BusinessEntity == null && entidad.BusinessEntityId == 0)
        {
            var newBe = new BusinessEntity
            {
                Rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.UtcNow
            };

            contexto.BusinessEntities.Add(newBe);
            await contexto.SaveChangesAsync();

            entidad.BusinessEntityId = newBe.BusinessEntityId;
            entidad.BusinessEntity = newBe;
        }
        if (string.IsNullOrWhiteSpace(entidad.Demographics))
        {
            entidad.Demographics = null;
        }
        else
        {
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(entidad.Demographics);
                entidad.Demographics = doc.OuterXml;
            }
            catch (XmlException)
            {
                throw new ArgumentException("Demographics must be well-formed XML with a single root element.");
            }
        }

        contexto.Stores.Update(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Store?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Stores.FirstOrDefaultAsync(s => s.BusinessEntityId == id);
    }

    public async Task<bool> BuscarDuplicado(string nombre, int idExcluido = 0)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Stores.AnyAsync(s => s.Name.ToLower().Equals(nombre.Trim().ToLower()) && s.BusinessEntityId != idExcluido);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var existe = await contexto.Stores.AnyAsync(s => s.BusinessEntityId == id);
        if (!existe) return false;

        var tieneClientes = await contexto.Customers.AnyAsync(c => c.StoreId == id);
        if (tieneClientes) return false;

        return await contexto.Stores.Where(s => s.BusinessEntityId == id).ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Store>> GetList(Expression<Func<Store, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Stores.Where(criterio).OrderBy(s => s.Name).ToListAsync();
    }
}