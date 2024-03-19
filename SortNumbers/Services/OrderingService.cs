using Microsoft.EntityFrameworkCore;
using SortNumbers.Data;
using SortNumbers.Models;

namespace SortNumbers.Services
{
    public class OrderingService : IOrderingService
    {
        private readonly ApplicationDbContext _context;

        public OrderingService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<SortResult> GetSortResultById(int id)
        {
            var result = await _context.SortResults
              .Include(sr => sr.SortedNumbers)
              .FirstOrDefaultAsync(sr => sr.Id == id);

            return result;
        }

        public async Task<SortResult> Sort(int[] numbers, bool isAcending)
        {
            var nums = new List<int>();
            var start = DateTime.Now;

            if (!isAcending)
            {
                nums = numbers.OrderByDescending(x => x).ToList();
            }
            else
            {
                nums = numbers.OrderBy(x => x).ToList();
            }

            var timeTaken = DateTime.Now - start;


            var entry = await _context.SortResults.AddAsync(new Models.SortResult
            {
                IsAscending = isAcending,
                TimeTaken = timeTaken,
                SortedNumbers = nums.Select(x => new SortedNumber { Value = x }).ToList()
            });

            _context.SaveChanges();

            return entry.Entity;
        }
    }

    public interface IOrderingService
    {
        Task<SortResult> Sort(int[] numbers, bool isAcending);

        Task<SortResult> GetSortResultById(int id);
    }
}
