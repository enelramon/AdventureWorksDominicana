using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Services
{
    public class SalesPersonService
    {
        private readonly Contexto _context;

        public SalesPersonService(Contexto context)
        {
            _context = context;
        }

        public async Task<List<SalesPerson>> GetSalesPeopleAsync()
        {
            try
            {
                return await _context.SalesPeople
                    .Include(s => s.Territory)
                    .Include(s => s.BusinessEntity) 
                        .ThenInclude(e => e.BusinessEntity) 
                    .OrderByDescending(s => s.ModifiedDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetSalesPeopleAsync: {ex.Message}");
                return new List<SalesPerson>();
            }
        }

        public async Task<SalesPerson?> GetSalesPersonByIdAsync(int id)
        {
            try
            {
                return await _context.SalesPeople
                    .Include(s => s.Territory)
                    .Include(s => s.BusinessEntity)
                        .ThenInclude(e => e.BusinessEntity)
                    .FirstOrDefaultAsync(s => s.BusinessEntityId == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> CreateAsync(SalesPerson salesPerson)
        {
            try
            {
                salesPerson.ModifiedDate = DateTime.Now;
                salesPerson.Rowguid = Guid.NewGuid();
                _context.SalesPeople.Add(salesPerson);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception) { return false; }
        }

        public async Task<bool> UpdateAsync(SalesPerson salesPerson)
        {
            try
            {
                salesPerson.ModifiedDate = DateTime.Now;
                _context.Entry(salesPerson).State = EntityState.Modified;
                _context.Entry(salesPerson).Property(x => x.Rowguid).IsModified = false;
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception) { return false; }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var salesPerson = await _context.SalesPeople.FindAsync(id);
                if (salesPerson == null) return false;
                _context.SalesPeople.Remove(salesPerson);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception) { return false; }
        }

        public async Task<List<Employee>> GetAvailableEmployeesAsync()
        {
            var currentSalesIds = await _context.SalesPeople.Select(s => s.BusinessEntityId).ToListAsync();
            return await _context.Employees
                .Include(e => e.BusinessEntity)
                .ThenInclude(p => p.EmailAddresses)
                .Where(e => !currentSalesIds.Contains(e.BusinessEntityId))
                .Take(20)
                .ToListAsync();
        }
    }
}