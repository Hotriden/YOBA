using YOBA_LibraryData.BLL.UOF.Repository;
using Moq;
using YOBA_LibraryData.BLL;
using YOBA_LibraryData.BLL.Entities.Products;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace YOBA_Products.Test
{
    [TestFixture]
    public class RepositoryTest
    {
        [Test]
        public void AddMethodTest()
        {
            var mock = new Mock<DbSet<Product>>();
            var mockContext = new Mock<YOBAContext>();

            mockContext.Setup(x => x.Products).Returns(mock.Object);
            var res = new ProductRepository(mockContext.Object);

            res.Add(new Product { Cost = 10, Price = 15, ProductId = 1, ProductName = "Tomato" });

            mockContext.Verify(s => s.Add(It.IsAny<Product>()), Times.Once());
            mockContext.Verify(s => s.SaveChanges(), Times.Once());
        }
    }
}
