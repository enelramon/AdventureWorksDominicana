using System.Linq.Expressions;
using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Services;

public class DepartmentService(IDbContextFactory<Contexto> DbFactory) : IService<Department, short>
{
    public async Task<bool> Guardar(Department entidad)
    {
        if (!await Existe(entidad.DepartmentId))
            return await Insertar(entidad);
        else
            return await Modificar(entidad);
    }

    private async Task<bool> Existe(short id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Departments.AnyAsync(d => d.DepartmentId == id);
    }

    private async Task<bool> Insertar(Department entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        entidad.ModifiedDate = DateTime.Now; // Aseguramos la fecha de auditoría
        contexto.Departments.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Department newDepartment)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        // Buscamos solo el departamento, sin incluir historiales
        var oldDepartment = await contexto.Departments
            .FirstOrDefaultAsync(d => d.DepartmentId == newDepartment.DepartmentId);

        if (oldDepartment != null)
        {
            // Solo actualizamos las propiedades puras del departamento (Nombre, Grupo)
            contexto.Departments.Entry(oldDepartment).CurrentValues.SetValues(newDepartment);
            oldDepartment.ModifiedDate = DateTime.Now;

            return await contexto.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<Department?> Buscar(short id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        // Traemos el departamento puro
        return await contexto.Departments
            .FirstOrDefaultAsync(d => d.DepartmentId == id);
    }

    public async Task<bool> Eliminar(short id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        // REGLA DE NEGOCIO: No permitir eliminar un departamento si tiene historial de empleados
        var tieneEmpleados = await contexto.EmployeeDepartmentHistories
            .AnyAsync(e => e.DepartmentId == id);

        if (tieneEmpleados)
        {
            // Lanzamos una excepción controlada para que tu UI pueda mostrar el ToastService
            throw new InvalidOperationException("No se puede eliminar un departamento que tiene o ha tenido empleados asignados.");
        }

        var oldDepartment = await contexto.Departments.FirstOrDefaultAsync(d => d.DepartmentId == id);
        if (oldDepartment != null)
        {
            contexto.Departments.Remove(oldDepartment);
            return await contexto.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<List<Department>> GetList(Expression<Func<Department, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Departments.Where(criterio).AsNoTracking().ToListAsync();
    }
}