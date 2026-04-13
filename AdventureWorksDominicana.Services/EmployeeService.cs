using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureWorksDominicana.Services;

public class EmployeeService(IDbContextFactory<Contexto> DbFactory) : IService<Employee, int>
{
    public async Task<bool> Guardar(Employee entidad)
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
        return await contexto.Employees.AnyAsync(e => e.BusinessEntityId == id);
    }

    private async Task<bool> Insertar(Employee entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        // INICIAMOS LA TRANSACCIÓN EXPLÍCITA  basicamente o se guarda todo o no se guarda nada
        await using var transaction = await contexto.Database.BeginTransactionAsync();

        try
        {
            //  Agregamos el empleado y todo su historial (Depto y Sueldo)
            contexto.Employees.Add(entidad);

            //  Guardamos en la base de datos
            var filasAfectadas = await contexto.SaveChangesAsync();

            //  Si todo salió perfecto, CONFIRMAMOS los cambios en SQL
            await transaction.CommitAsync();

            return filasAfectadas > 0;
        }
        catch (Exception)
        {
            // SI ALGO FALLA (ej. se va la luz, o la base de datos rechaza la tanda),
            // REVERTIMOS TODO. Así evitamos que el empleado se cree a medias sin sueldo.
            await transaction.RollbackAsync();

            return false;
        }
    }

    private async Task<bool> Modificar(Employee entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Employees.Update(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Employee?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        // Includes necesarios para la pantalla de Edición
        return await contexto.Employees
            .Include(e => e.BusinessEntity)
            .Include(e => e.EmployeeDepartmentHistories) // Trae el historial de departamentos
                .ThenInclude(edh => edh.Department)      // Trae el nombre del departamento
            .Include(e => e.EmployeeDepartmentHistories)
                .ThenInclude(edh => edh.Shift)           // Trae la información del turno (tanda)
            .Include(e => e.EmployeePayHistories)        // Trae el historial de sueldos
            .FirstOrDefaultAsync(e => e.BusinessEntityId == id);
    }

    public async Task<Employee?> Buscar(int id, int idExcluido = 0)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Employees
            .Include(e => e.BusinessEntity)
            .FirstOrDefaultAsync(e => e.BusinessEntityId == id && e.BusinessEntityId != idExcluido);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Employees.Where(e => e.BusinessEntityId == id).ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Employee>> GetList(Expression<Func<Employee, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Employees
            .Include(e => e.BusinessEntity) // Trae Nombre y Apellido
            .Include(e => e.EmployeeDepartmentHistories) //  Para saber si tiene depto
            .Include(e => e.EmployeePayHistories)        // Para saber si tiene sueldo
            .Where(criterio)
            .ToListAsync();
    }
}