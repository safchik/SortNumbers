using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SortNumbers.Data;
using SortNumbers.Models;
using SortNumbers.Services;
using SortNumbers.ViewModels;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace SortNumbers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderingService _orderingService;

        public HomeController(ILogger<HomeController> logger, IOrderingService orderingService)
        {
            _logger = logger;
            _orderingService = orderingService;
        }

        public IActionResult Index()
        {
            return View(new AddSequenceViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> PostNumbers(AddSequenceViewModel model)
        {
            var nums = new List<int>();

            if (model.Ordering != "desc" && model.Ordering != "asc")
            {
                ModelState.TryAddModelError(nameof(AddSequenceViewModel.Ordering), "Invalid sort order");
                return View("Index", model);
            }

            foreach (var num in model.Numbers.Split(','))
            {
                if (!int.TryParse(num, out var parsedNumber))
                {
                    ModelState.TryAddModelError(nameof(AddSequenceViewModel.Numbers), $"Number {num} is not a valid number. Valid number range is between {int.MinValue} and {int.MaxValue}");
                    return View("Index", model);
                }

                nums.Add(parsedNumber);
            }

            var sortedResult = await _orderingService.Sort(nums.ToArray(), model.Ordering == "asc");

           

            return View("SortResult", sortedResult);
        }

        [HttpGet("Download/{id}")]
        public async Task<IActionResult> DownloadAsJson(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var sortResult = await _orderingService.GetSortResultById(id);

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