using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL;
using YOBA_LibraryData.BLL.Entities.Products;
using YOBA_LibraryData.BLL.UOF.Repository;
using FluentAssertions;


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
            var res = new ProductRepository(mockContext.Object);

            res.Add(new Product() { Cost = 10, Price = 15, ProductId = 1, ProductName = "Tomato" });
            res.Add(new Product() { Cost = 15, Price = 20, ProductId = 2, ProductName = "Orange" });

            mockContext.Verify(s => s.Add(It.IsAny<Product>()), Times.AtLeast(2));
            mockContext.Verify(s => s.SaveChanges(), Times.AtLeast(2));
        }

        [Test]
        public void ProductRepo_GetByID()
        {
            var data = new List<Product>()
            {
                new Product
                {
                    Cost=10,
                    Price=15,
                    ProductId=5,
                    ProductName="Pasta"
                },
                new Product
                {
                    Cost=15,
                    Price=25,
                    ProductId=12,
                    ProductName="Papper"
                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Product>>();
            mockDbSet.As<IQueryable<Product>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Product>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Product>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Product>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Products).Returns(mockDbSet.Object);

            var repo = new ProductRepository(context.Object);
            var result = repo.GetById(12);

            Assert.IsTrue(result.ProductName == "Papper");
        }

        [Test]
        public void ProductRepo_GetAll()
        {
            var data = new List<Product>()
            {
                new Product
                {
                    Cost=10,
                    Price=15,
                    ProductId=5,
                    ProductName="Pasta"
                },
                new Product
                {
                    Cost=15,
                    Price=25,
                    ProductId=12,
                    ProductName="Papper"
                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Product>>();
            mockDbSet.As<IQueryable<Product>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Product>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Product>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Product>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(c => c.Products).Returns(mockDbSet.Object);

            var repo = new ProductRepository(context.Object);
            var result = repo.GetAll().ToList();

            result.Should().AllBeOfType(typeof(Product));
            result.Should().HaveCount(2);
            result.Should().Contain(x => x.ProductName == "Pasta");
        }

        [Test]
        public void ProductRepo_Delete()
        {
            var data = new List<Product>()
            {
                new Product
                {
                    Cost=10,
                    Price=15,
                    ProductId=5,
                    ProductName="Pasta"
                },
                new Product
                {
                    Cost=15,
                    Price=25,
                    ProductId=12,
                    ProductName="Papper"
                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Product>>();
            mockDbSet.As<IQueryable<Product>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Product>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Product>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Product>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(c => c.Products).Returns(mockDbSet.Object);
            var repo = new ProductRepository(context.Object);
            repo.Delete(new Product() { Cost = 15, Price = 25, ProductId = 12, ProductName = "Papper" });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public void ProductRepo_Update()
        {
            var data = new List<Product>()
            {
                new Product
                {
                    Cost=10,
                    Price=15,
                    ProductId=5,
                    ProductName="Pasta"
                },
                new Product
                {
                    Cost=15,
                    Price=25,
                    ProductId=12,
                    ProductName="Papper"
                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Product>>();
            mockDbSet.As<IQueryable<Product>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Product>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Product>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Product>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(c => c.Products).Returns(mockDbSet.Object);
            var repo = new ProductRepository(context.Object);
            repo.Change(new Product() { Cost = 15, Price = 25, ProductId = 12, ProductName = "Nugget" });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }
    }
}