using Api.Data.Repository.Queries;
using Api.Domain.Enum;
using Api.Infraestructura.Context;
using Api.Infraestructura.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Api.Test.Queries
{
    public class GetCartQueryTest
    {
        private readonly CartQuery _cartQuery;
        private readonly Mock<ApiContext> _mockContext;
        private readonly Mock<DbSet<Cart>> _mockCartDbSet;
        private readonly Mock<DbSet<Product>> _mockProductDbSet;

        public GetCartQueryTest()
        {
            _mockContext = new Mock<ApiContext>();
            _mockCartDbSet = new Mock<DbSet<Cart>>();
            _mockProductDbSet = new Mock<DbSet<Product>>();

            _mockContext.Setup(m => m.Carts).Returns(_mockCartDbSet.Object);
            _mockContext.Setup(m => m.Products).Returns(_mockProductDbSet.Object);

            _cartQuery = new CartQuery(_mockContext.Object, null); // Assuming HttpClient is not used directly in the tested methods
        }


        [Fact]
        public async Task GetCartAsync_CartFound_ReturnsSuccess()
        {
            // Arrange
            var expectedCart = new Cart { UserId = 1, Products = new List<CartItem>() };
            var data = new List<Cart> { expectedCart }.AsQueryable();
            var mockSet = new Mock<DbSet<Cart>>();
            mockSet.As<IQueryable<Cart>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Cart>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Cart>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Cart>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            _mockContext.Setup(m => m.Carts).Returns(mockSet.Object);

            // Act
            var result = await _cartQuery.GetCartAsync(1);

            // Assert
            Assert.Equal(StatusType.SUCCESS, result.Status);
            Assert.Equal("Carrito encontrado", result.Message);
            Assert.NotNull(result.Data);
        }
    }
}
