using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdventureWorksDominicana.Services
{
    public class TransactionHistoryService(IDbContextFactory<Contexto> DbContextFactory) : IService<TransactionHistory, int>
    {
        public async Task<TransactionHistory?> Buscar(int id)
        {
            await using var contexto = await DbContextFactory.CreateDbContextAsync();
            return await contexto.TransactionHistories.FirstOrDefaultAsync(s => s.TransactionId == id);
        }

        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await DbContextFactory.CreateDbContextAsync();
            var filasAfectadas = await contexto.TransactionHistories.Where(s => s.TransactionId == id).ExecuteDeleteAsync();
            return filasAfectadas > 0;
        }
        public async Task<List<TransactionHistory>> GetList(Expression<Func<TransactionHistory, bool>> criterio)
        {
            await using var contexto = await DbContextFactory.CreateDbContextAsync();
            return await contexto.TransactionHistories.Include(e => e.Product).Where(criterio).AsNoTracking().ToListAsync();
        }
        public async Task<bool> Guardar(TransactionHistory transaction)
        {
            if (!await Existe(transaction.TransactionId))
            {
                return await Insertar(transaction);

            }
            else
            {
                return await Modificar(transaction);
            }
        }
        public async Task<bool> Existe(int id)
        {
            await using var contexto = await DbContextFactory.CreateDbContextAsync();
            return await contexto.TransactionHistories.AnyAsync(s => s.TransactionId == id);
        }
        public async Task<bool> Insertar(TransactionHistory transaction)
        {
            await using var contexto = await DbContextFactory.CreateDbContextAsync();
            contexto.TransactionHistories.Add(transaction);
            return await contexto.SaveChangesAsync() > 0;
        }
        public async Task<bool> Modificar(TransactionHistory transaction)
        {
            await using var contexto = await DbContextFactory.CreateDbContextAsync();
            contexto.TransactionHistories.Update(transaction);
            return await contexto.SaveChangesAsync() > 0;
        }
    
    }
}
