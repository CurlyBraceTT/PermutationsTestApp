using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PermutationsTestApp.Model;
using PermutationsTestApp.Services;
using Xunit;

namespace PermutationsTestApp.Tests
{
    public class PermutationsServiceTests
    {
        private readonly PermutationsContext _context;

        public PermutationsServiceTests()
        {
            var options = new DbContextOptionsBuilder<PermutationsContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
            _context = new PermutationsContext(options);
        }

        [Theory]
        [InlineData(1, "a")]
        [InlineData(2, "46")]
        [InlineData(3, "aab")]
        [InlineData(3, "baa")]
        [InlineData(5, "22322")]
        [InlineData(6, "xyz")]
        [InlineData(12, "xyaa")]
        public async Task ShouldCalculateCorrectly(int expectedCount, string value)
        {
            var service = new PermutationsService(_context);
            var result = await service.CalculateAsync(value);
            Assert.Equal(expectedCount, result.PermutationCount);
        }

        [Theory]
        [InlineData("qwerty123")]
        public async Task ShouldStoreCalculated(string value)
        {
            var service = new PermutationsService(_context);
            var result = await service.CalculateAsync(value);
            var stored = await service.GetAsync(value);

            Assert.NotNull(stored);
            Assert.Equal(result.Value, stored.Value);
        }
    }
}
