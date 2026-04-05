using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksDominicana.Services
{
    public class ProductReviewService
    {
        private readonly IDbContextFactory<Contexto> _factory;

        public ProductReviewService(IDbContextFactory<Contexto> factory)
        {
            _factory = factory;
        }

        public async Task<bool> CreateAsync(ProductReview review)
        {
            try
            {
                await using var context = await _factory.CreateDbContextAsync();

                review.Product = null!;
                review.ReviewDate = DateTime.Now;
                review.ModifiedDate = DateTime.Now;

                context.ProductReviews.Add(review);

                var resultado = await context.SaveChangesAsync() > 0;

                Console.WriteLine("GUARDADO OK");

                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine("******** ERROR ********");
                Console.WriteLine(ex.ToString());
                Console.WriteLine("************************");

                throw; 
            }
        }
        public async Task<List<ProductReview>> GetReviewsAsync()
        {
            await using var context = await _factory.CreateDbContextAsync();
            return await context.ProductReviews
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductReview?> GetReviewByIdAsync(int id)
        {
            await using var context = await _factory.CreateDbContextAsync();
            return await context.ProductReviews
                .FirstOrDefaultAsync(p => p.ProductReviewId == id);
        }

        public async Task<bool> UpdateAsync(ProductReview review)
        {
            try
            {
                await using var context = await _factory.CreateDbContextAsync();
                review.ModifiedDate = DateTime.Now;
                review.Product = null!; 
                context.Update(review);
                return await context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var affectedRows = await context.ProductReviews
                .Where(p => p.ProductReviewId == id)
                .ExecuteDeleteAsync();
            return affectedRows > 0;
        }
    }
}