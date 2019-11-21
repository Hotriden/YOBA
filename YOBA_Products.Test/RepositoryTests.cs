using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using YOBA_LibraryData.BLL;
using YOBA_LibraryData.BLL.Entities.Products;
using YOBA_LibraryData.BLL.UOF.Repository;

namespace YOBA_Products.Test
{
    [TestFixture]
    public class RepositoryTests
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
