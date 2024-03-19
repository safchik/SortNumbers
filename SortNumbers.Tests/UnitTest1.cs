using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SortNumbers.Data;
using SortNumbers.Services;
using System.Data.Common;

namespace SortNumbers.Tests
{
    public class OrderingServiceTests
    {
        private OrderingService _sut;

        public OrderingServiceTests()
        {
            var dbContextoptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("ApplicationDbContextTest")
            .Options;

            var dbContext = new ApplicationDbContext(dbContextoptions);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            _sut = new OrderingService(dbContext);
        }
        [Fact]
        public async Task Should_Order()
        {
            var numbers = new[] { 1, 2, 155, 4 };
            var expected = new[] { 1, 2, 4, 155 };
            bool isAscending = true;

            var result = await _sut.Sort(numbers, isAscending);

            Assert.NotNull(result);
            Assert.True(result.IsAscending);
            Assert.True(result.TimeTaken > TimeSpan.Zero);
            Assert.Equivalent(expected, result.SortedNumbers.Select(x => x.Value).ToArray());
        }
    }
}