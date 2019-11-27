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
    class ProductGroupRepositoryTests
    {
        [Test]
        public void ProductGroupRepo_Add()
        {
            var mockDbSet = new Mock<DbSet<ProductGroup>>();
            var mockContext = new Mock<YOBAContext>();

            mockContext.Setup(c => c.ProductGroups).Returns(mockDbSet.Object);
            var res = new ProductGroupRepository(mockContext.Object);

            res.Add(new ProductGroup() { GroupId=1, GroupName="Tools" });
            res.Add(new ProductGroup() { GroupId = 3, GroupName = "Pumps" });

            mockContext.Verify(s => s.Add(It.IsAny<ProductGroup>()), Times.AtLeast(2));
            mockContext.Verify(s => s.SaveChanges(), Times.AtLeast(2));
        }

        [Test]
        public void ProductGroupRepo_GetByID()
        {
            var data = new List<ProductGroup>()
            {
                new ProductGroup { GroupId=1, GroupName="Tools" },
                new ProductGroup { GroupId=2, GroupName="Pumps" },
                new ProductGroup { GroupId=10,GroupName="Medicine"}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<ProductGroup>>();
            mockDbSet.As<IQueryable<ProductGroup>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<ProductGroup>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<ProductGroup>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<ProductGroup>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.ProductGroups).Returns(mockDbSet.Object);

            var repo = new ProductGroupRepository(context.Object);
            var result = repo.GetById(10);

            Assert.IsTrue(result.GroupName == "Medicine");
        }

        [Test]
        public void ProductGroupRepo_GetAll()
        {
            var data = new List<ProductGroup>()
            {
                new ProductGroup { GroupId=1, GroupName="Tools" },
                new ProductGroup { GroupId=2, GroupName="Pumps" },
                new ProductGroup { GroupId=10,GroupName="Medicine"}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<ProductGroup>>();
            mockDbSet.As<IQueryable<ProductGroup>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<ProductGroup>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<ProductGroup>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<ProductGroup>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.ProductGroups).Returns(mockDbSet.Object);

            var repo = new ProductGroupRepository(context.Object);
            var result = repo.GetAll().ToList();

            result.Should().AllBeOfType(typeof(ProductGroup));
            result.Should().HaveCount(3);
            result.Should().Contain(x => x.GroupName == "Medicine");
        }

        [Test]
        public void ProductRepo_Delete()
        {
            var data = new List<ProductGroup>()
            {
                new ProductGroup { GroupId=1, GroupName="Tools" },
                new ProductGroup { GroupId=2, GroupName="Pumps" },
                new ProductGroup { GroupId=10,GroupName="Medicine"}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<ProductGroup>>();
            mockDbSet.As<IQueryable<ProductGroup>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<ProductGroup>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<ProductGroup>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<ProductGroup>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.ProductGroups).Returns(mockDbSet.Object);

            var repo = new ProductGroupRepository(context.Object);
            repo.Delete(new ProductGroup() { GroupId=1, GroupName="Tools" });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public void ProductRepo_Update()
        {
            var data = new List<ProductGroup>()
            {
                new ProductGroup { GroupId=1, GroupName="Tools" },
                new ProductGroup { GroupId=2, GroupName="Pumps" },
                new ProductGroup { GroupId=10,GroupName="Medicine"}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<ProductGroup>>();
            mockDbSet.As<IQueryable<ProductGroup>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<ProductGroup>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<ProductGroup>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<ProductGroup>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.ProductGroups).Returns(mockDbSet.Object);

            var repo = new ProductGroupRepository(context.Object);
            repo.Change(new ProductGroup() { GroupId = 10, GroupName = "Snacks" });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }
    }
}
