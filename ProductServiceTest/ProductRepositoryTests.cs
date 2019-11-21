using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using YOBA_LibraryData.BLL;
using YOBA_LibraryData.BLL.Entities.Products;
using YOBA_LibraryData.BLL.UOF.Repository;

namespace ProductServiceTest
{
    public class ProductRepositoryTests
    {

        [Test]
        public void ProductRepo_Add()
        {
            var mockDbSet = new Mock<DbSet<Product>>();
            var mockContext = new Mock<YOBAContext>();

            mockContext.Setup(c => c.Products).Returns(mockDbSet.Object);
            var sut = new ProductRepository(mockContext.Object);

            sut.Add(new Product() { Cost = 10, Price = 15, ProductId = 1, ProductName = "Tomato" });
            sut.Add(new Product() { Cost = 15, Price = 20, ProductId = 2, ProductName = "Orange" });

            mockContext.Verify(s => s.Add(It.IsAny<Product>()), Times.AtLeast(2));
            mockContext.Verify(s => s.SaveChanges(), Times.AtLeast(2));
        }
    }
}