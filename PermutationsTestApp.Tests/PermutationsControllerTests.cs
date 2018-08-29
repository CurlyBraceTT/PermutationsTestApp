using System.Threading.Tasks;
using AutoMapper;
using Moq;
using PermutationsTestApp.Controllers;
using PermutationsTestApp.Model;
using PermutationsTestApp.Services;
using PermutationsTestApp.ViewModel;
using Xunit;

namespace PermutationsTestApp.Tests
{
    public class PermutationsControllerTests
    {
        private readonly PermutationsController _controller;
        private readonly Mock<IPermutationsService> _serviceMock;

        public PermutationsControllerTests()
        {
            _serviceMock = new Mock<IPermutationsService>();
            _serviceMock.Setup(s => s.CalculateAsync(It.IsAny<string>())).Returns((string value) =>
            {
                return Task.FromResult(new Element
                {
                    Value = value
                });
            });
            _serviceMock.Setup(s => s.GetAsync(It.IsAny<string>())).Returns((string value) =>
            {
                return Task.FromResult(new Element
                {
                    Value = value
                });
            });

            var mapper = new MapperConfiguration((config =>
            {
                config.AddProfile<ViewModelProfile>();
            })).CreateMapper();

            _controller = new PermutationsController(_serviceMock.Object, mapper);
        }

        [Theory]
        [InlineData("123", "456", "aa", "ab")]
        public async Task ShouldPostElements(params string[] values)
        {
            var result = await _controller.Post(values);

            Assert.NotNull(result);
            Assert.Equal(values.Length, result.Count);
        }

        [Theory]
        [InlineData("123")]
        public async Task ShouldGetElement(string value)
        {
            var result = await _controller.Get(value);
            Assert.NotNull(result);
        }
    }
}
