using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SortNumbers.Data;
using SortNumbers.Models;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace SortNumbers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostNumbers(string numbers, string sortOrder)
        {
            int[] nums = numbers.Split(',').Select(int.Parse).ToArray();
            var start = DateTime.Now;

            if (sortOrder == "desc")
            {
                nums = nums.OrderByDescending(x => x).ToArray();
            }
            else
            {
                nums = nums.OrderBy(x => x).ToArray();
            }

            var timeTaken = DateTime.Now - start;
            

            var entry = await _context.SortResults.AddAsync(new Models.SortResult
            {
                IsAscending = sortOrder != "desc",
                TimeTaken = timeTaken,
                SortedNumbers = nums.Select(x => new SortedNumber { Value = x }).ToList()
            });

            _context.SaveChanges();

            return RedirectToAction("SortResult", "Home", entry.Entity);
        }


        public async Task<IActionResult> SortResult(int id)
        {
            var sortResult = await _context.SortResults
                .Include(sr => sr.SortedNumbers)
                .FirstOrDefaultAsync(sr => sr.Id == id);

            if (sortResult == null)
            {
                return NotFound();
            }

            return View(sortResult);
        }

        [HttpGet("Download/{id}")]
        public async Task<IActionResult> DownloadAsJson(int id)
        {
            var sortResult = await _context.SortResults
              .Include(sr => sr.SortedNumbers)
              .FirstOrDefaultAsync(sr => sr.Id == id);

            if (sortResult == null)
            {
                return NotFound();
            }

            var json = JsonSerializer.Serialize(new
            {
                Id = sortResult.Id,
                SortDirection = sortResult.IsAscending ? "Acending" : "Descending",
                TimeTaken = sortResult.TimeTaken,
                Values = sortResult.SortedNumbers.Select(x => x.Value).ToArray()
            });

            return File(Encoding.UTF8.GetBytes(json), "application/octet-stream", $"{id}.json");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}