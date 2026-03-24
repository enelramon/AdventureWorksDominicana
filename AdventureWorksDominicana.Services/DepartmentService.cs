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
        contexto.Departments.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }
    private async Task<bool> Modificar(Department newDepartment)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var oldDepartment = await contexto.Departments.Include(l => l.EmployeeDepartmentHistories).FirstOrDefaultAsync(d => d.DepartmentId == newDepartment.DepartmentId);
        if (oldDepartment != null)
        {
            contexto.EmployeeDepartmentHistories.RemoveRange(oldDepartment.EmployeeDepartmentHistories);
            contexto.Departments.Entry(oldDepartment).CurrentValues.SetValues(newDepartment);
            oldDepartment.ModifiedDate = DateTime.Now;
        }
        foreach(var newEmployee in newDepartment.EmployeeDepartmentHistories)
        {
            oldDepartment?.EmployeeDepartmentHistories.Add(
               new EmployeeDepartmentHistory
               {
                   BusinessEntityId = newEmployee.BusinessEntityId,
                   ShiftId = newEmployee.ShiftId,
                   StartDate = newEmployee.StartDate,
                   EndDate = newEmployee.EndDate,
                   ModifiedDate = DateTime.Now
               }
            );
        }
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<Department?> Buscar(short id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Departments.Include(h => h.EmployeeDepartmentHistories).FirstOrDefaultAsync(d => d.DepartmentId == id);
    }
    public async Task<bool> Eliminar(short id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var oldDepartment = await contexto.Departments.Include(l => l.EmployeeDepartmentHistories).FirstOrDefaultAsync(d => d.DepartmentId == id);
        if (oldDepartment != null)
        {
            contexto.EmployeeDepartmentHistories.RemoveRange(oldDepartment.EmployeeDepartmentHistories);
            contexto.Departments.Remove(oldDepartment);
        }
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<List<Department>> GetList(Expression<Func<Department, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Departments.Where(criterio).AsNoTracking().ToListAsync();
    }
}