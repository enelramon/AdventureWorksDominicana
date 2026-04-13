using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AdventureWorksDominicana.Services;

public class PersonService(IDbContextFactory<Contexto> DbFactory) : IService<Person, int>
{
    public async Task<Person?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.People
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.BusinessEntityId == id);
    }

    public async Task<bool> Guardar(Person entidad)
    {
        if (!await Existe(entidad.BusinessEntityId))
            return await Insertar(entidad);
        else
            return await Modificar(entidad);
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.People
            .AnyAsync(p => p.BusinessEntityId == id);
    }

    private async Task<bool> Insertar(Person entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        if (entidad.Rowguid == Guid.Empty)
            entidad.Rowguid = Guid.NewGuid();

        entidad.ModifiedDate = DateTime.Now;

        entidad.BusinessEntity = new BusinessEntity
        {
            Rowguid = Guid.NewGuid(),
            ModifiedDate = DateTime.Now
        };

        contexto.People.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Person entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        entidad.ModifiedDate = DateTime.Now;

        contexto.People.Update(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var eliminados = await contexto.People
            .Where(p => p.BusinessEntityId == id)
            .ExecuteDeleteAsync();

        return eliminados > 0;
    }

    public async Task<List<Person>> GetList(Expression<Func<Person, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.People
            .Where(criterio).Include(a => a.EmailAddresses)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Person>> BuscarPorNombre(string busqueda)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        if (string.IsNullOrWhiteSpace(busqueda))
            return new List<Person>();

        busqueda = busqueda.Trim().ToLower();

        return await contexto.People
            .AsNoTracking()
            .Where(p =>
                (p.FirstName != null && p.FirstName.ToLower().Contains(busqueda)) ||
                (p.MiddleName != null && p.MiddleName.ToLower().Contains(busqueda)) ||
                (p.LastName != null && p.LastName.ToLower().Contains(busqueda)))
            .OrderBy(p => p.FirstName)
            .ThenBy(p => p.LastName)
            .ToListAsync();
    }

    public async Task<Person?> BuscarPorEmailCompleto(string email)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.People
            .AsNoTracking()
            .Include(p => p.EmailAddresses)
            .Include(p => p.PersonPhones)
            .FirstOrDefaultAsync(p => p.EmailAddresses.Any(e => e.EmailAddress1 == email));
    }
    public async Task<bool> ActualizarPerfilCompleto(int id, Person datosNuevos)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        // Aquí busco la persona con sus relaciones actuales y activando el Tracking
        var personaDB = await contexto.People
            .Include(p => p.EmailAddresses)
            .Include(p => p.PersonPhones)
            .FirstOrDefaultAsync(p => p.BusinessEntityId == id);

        if (personaDB == null) return false;

        // Actualizo datos 
        personaDB.FirstName = datosNuevos.FirstName;
        personaDB.MiddleName = datosNuevos.MiddleName;
        personaDB.LastName = datosNuevos.LastName;
        personaDB.EmailPromotion = datosNuevos.EmailPromotion;
        personaDB.ModifiedDate = DateTime.Now;

        // sINCRONIZO correos electronicos
        contexto.EmailAddresses.RemoveRange(personaDB.EmailAddresses);

        foreach (var email in datosNuevos.EmailAddresses)
        {
            personaDB.EmailAddresses.Add(new EmailAddress
            {
                BusinessEntityId = id,
                EmailAddress1 = email.EmailAddress1,
                Rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Now
            });
        }

        // Sincronizo teléfonos
        contexto.PersonPhones.RemoveRange(personaDB.PersonPhones);

        foreach (var phone in datosNuevos.PersonPhones)
        {
            personaDB.PersonPhones.Add(new PersonPhone
            {
                BusinessEntityId = id,
                PhoneNumber = phone.PhoneNumber,
                PhoneNumberTypeId = phone.PhoneNumberTypeId,
                ModifiedDate = DateTime.Now
            });
        }

        // Guardo todos los cambios
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<PersonCreditCard>> ObtenerTarjetasPorPersonaId(int businessEntityId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.PersonCreditCards
            .AsNoTracking()
            .Include(pcc => pcc.CreditCard) 
            .Where(pcc => pcc.BusinessEntityId == businessEntityId)
            .ToListAsync();
    }

    public async Task<bool> AgregarTarjetaAPersona(int businessEntityId, CreditCard nuevaTarjeta)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var existePersona = await contexto.People
            .AnyAsync(p => p.BusinessEntityId == businessEntityId);

        if (!existePersona) return false;

        nuevaTarjeta.ModifiedDate = DateTime.Now;

        var personCreditCard = new PersonCreditCard
        {
            BusinessEntityId = businessEntityId,
            CreditCard = nuevaTarjeta,
            ModifiedDate = DateTime.Now
        };

        contexto.PersonCreditCards.Add(personCreditCard);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> EliminarTarjetaDePersona(int businessEntityId, int creditCardId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var vinculoTarjeta = await contexto.PersonCreditCards
            .FirstOrDefaultAsync(pcc => pcc.BusinessEntityId == businessEntityId && pcc.CreditCardId == creditCardId);

        if (vinculoTarjeta == null) return false;

        contexto.PersonCreditCards.Remove(vinculoTarjeta);

        return await contexto.SaveChangesAsync() > 0;
    }

    public bool EsCedulaValida(string cedula)
    {
        //ALGORITMO FUNCIONAL DE VALIDACION DE CEDULA DOMINICANA.
        if (string.IsNullOrWhiteSpace(cedula)) return false;

        cedula = cedula.Replace("-", "").Replace(" ", "");

        if (cedula.Length != 11 || !long.TryParse(cedula, out _)) return false;

        int suma = 0;
        int[] pesos = { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };

        for (int i = 0; i < 10; i++)
        {
            int digito = int.Parse(cedula[i].ToString()) * pesos[i];

            if (digito > 9)
                digito = (digito / 10) + (digito % 10);

            suma += digito;
        }

        int verificadorCalculado = (10 - (suma % 10)) % 10;
        int verificadorReal = int.Parse(cedula[10].ToString());

        return verificadorCalculado == verificadorReal;
    }

}