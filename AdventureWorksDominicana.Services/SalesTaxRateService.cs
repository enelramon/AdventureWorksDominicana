using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Services
{
    public class SalesTaxRateService
    {
        private readonly Contexto _context;

        public SalesTaxRateService(Contexto context)
        {
            _context = context;
        }

        public async Task<List<SalesTaxRate>> GetSalesTaxRatesAsync()
        {
            return await _context.SalesTaxRates
                .Include(s => s.StateProvince)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public async Task<List<StateProvince>> GetStateProvincesAsync()
        {
            return await _context.StateProvinces.OrderBy(s => s.Name).ToListAsync();
        }

        public async Task<SalesTaxRate?> GetSalesTaxRateByIdAsync(int id)
        {
            return await _context.SalesTaxRates
                .Include(s => s.StateProvince)
                .FirstOrDefaultAsync(x => x.SalesTaxRateId == id);
        }

        public async Task<bool> CreateAsync(SalesTaxRate taxRate)
        {
            try
            {
                taxRate.ModifiedDate = DateTime.Now;
                taxRate.Rowguid = Guid.NewGuid();
                _context.SalesTaxRates.Add(taxRate);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception) { return false; }
        }

        public async Task<bool> UpdateAsync(SalesTaxRate taxRate)
        {
            try
            {
                taxRate.ModifiedDate = DateTime.Now;
                _context.Entry(taxRate).State = EntityState.Modified;
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception) { return false; }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var registro = await _context.SalesTaxRates.FindAsync(id);
                if (registro == null) return false;

                _context.SalesTaxRates.Remove(registro);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception) { return false; }
        }
    }
}