using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdventureWorksDominicana.Services;

public class UserService(IDbContextFactory<Contexto> DbFactory) : IService<AspNetUser, string>
{
    public async Task<AspNetUser?> Buscar(string id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.AspNetUsers.Include(x => x.Roles).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> Eliminar(string id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var usuario = await contexto.AspNetUsers.FirstOrDefaultAsync(u => u.Id == id);
        if (usuario == null) return false;

        // Seteamos una fecha en el futuro lejano para marcarlo como borrado
        usuario.LockoutEnd = DateTimeOffset.MaxValue;
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> RestaurarUsuario(string id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var usuario = await contexto.AspNetUsers
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.Id == id);

        if (usuario == null) return false;

        usuario.LockoutEnd = null;
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<List<AspNetUser>> GetList(Expression<Func<AspNetUser, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.AspNetUsers.Where(criterio).OrderBy(t => t.UserName).Include(u => u.Roles).ToListAsync();
    }

    public async Task<bool> Guardar(AspNetUser entidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var usuarioDb = await contexto.AspNetUsers
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Id == entidad.Id);

        if (usuarioDb == null) return false;

        usuarioDb.Email = entidad.Email;
        usuarioDb.NormalizedEmail = entidad.Email.ToUpper();
        usuarioDb.LockoutEnabled = entidad.LockoutEnabled;
        
        usuarioDb.Roles.Clear();

        var nuevoRolId = entidad.Roles.FirstOrDefault()?.Id;

        if (!string.IsNullOrEmpty(nuevoRolId))
        {
            var rolDb = await contexto.AspNetRoles.FindAsync(nuevoRolId);
            if (rolDb != null)
            {
                usuarioDb.Roles.Add(rolDb);
            }
        }
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<List<AspNetRole>> GetListRoles(Expression<Func<AspNetRole, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.AspNetRoles.Where(criterio).OrderBy(t => t.Id).ToListAsync();
    }
    public async Task<List<AspNetUser>> GetListConEliminados(bool mostrarSoloEliminados, string filtro, string valor)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        IQueryable<AspNetUser> query = contexto.AspNetUsers.Include(u => u.Roles);

        if (mostrarSoloEliminados)
        {
            query = query.IgnoreQueryFilters()
                         .Where(u => u.LockoutEnd > DateTimeOffset.Now.AddYears(100));
        }
        else
        {
            query = query.Where(u => u.LockoutEnd == null || u.LockoutEnd <= DateTimeOffset.Now.AddYears(100));
        }

        if (!string.IsNullOrWhiteSpace(valor))
        {
            valor = valor.ToLower();
            if (filtro == "Nombre")
                query = query.Where(u => u.UserName.ToLower().Contains(valor));
            else if (filtro == "Rol")
                query = query.Where(u => u.Roles.Any(r => r.Name.ToLower().Contains(valor)));
        }

        return await query.ToListAsync();
    }
}
