using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL;
using YOBA_LibraryData.BLL.UOF.Repository;
using FluentAssertions;
using YOBA_LibraryData.BLL.Entities.Finance;

namespace ProductServiceTest
{
    public class TaxRepositoryTests
    {
        [Test]
        public void TaxRepo_Add()
        {
            var mockDbSet = new Mock<DbSet<Tax>>();
            var mockContext = new Mock<YOBAContext>();

            mockContext.Setup(c => c.Taxes).Returns(mockDbSet.Object);
            var res = new TaxRepository(mockContext.Object);

            res.Add(new Tax() { Id = 1, Name = "Freight", Percent = 20 });

            mockContext.Verify(s => s.Add(It.IsAny<Tax>()), Times.Once());
            mockContext.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public void TaxRepo_GetByID()
        {
            var data = new List<Tax>() {
                new Tax() { Id=1, Name = "Freight", Percent = 20},
                new Tax() { Id=101, Name = "VAT", Percent = 18}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Tax>>();
            mockDbSet.As<IQueryable<Tax>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Tax>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Tax>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Tax>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Taxes).Returns(mockDbSet.Object);

            var repo = new TaxRepository(context.Object);
            var result = repo.GetById(101);

            Assert.IsTrue(result.Name == "VAT");
        }

        [Test]
        public void TaxRepo_GetAll()
        {
            var data = new List<Tax>() {
                new Tax() { Id=1, Name = "Freight", Percent = 20},
                new Tax() { Id=101, Name = "VAT", Percent = 18},
                new Tax(){Id=3, Name="Medicine", Percent=3}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Tax>>();
            mockDbSet.As<IQueryable<Tax>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Tax>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Tax>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Tax>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Taxes).Returns(mockDbSet.Object);

            var repo = new TaxRepository(context.Object);
            var result = repo.GetAll().ToList();

            result.Should().AllBeOfType(typeof(Tax));
            result.Should().HaveCount(3);
            result.Should().Contain(x => x.Name == "VAT");
        }

        [Test]
        public void TaxRepo_Delete()
        {
            var data = new List<Tax>() {
                new Tax() { Id=1, Name = "Freight", Percent = 20},
                new Tax() { Id=101, Name = "VAT", Percent = 18},
                new Tax(){Id=3, Name="Medicine", Percent=3}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Tax>>();
            mockDbSet.As<IQueryable<Tax>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Tax>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Tax>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Tax>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Taxes).Returns(mockDbSet.Object);

            var repo = new TaxRepository(context.Object);
            repo.Delete(new Tax() {
                Id = 3,
                Name = "Medicine",
                Percent = 3 });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public void TaxRepo_Update()
        {
            var data = new List<Tax>() {
                new Tax() { Id=1, Name = "Freight", Percent = 20},
                new Tax() { Id=101, Name = "VAT", Percent = 18},
                new Tax(){Id=3, Name="Medicine", Percent=3}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Tax>>();
            mockDbSet.As<IQueryable<Tax>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Tax>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Tax>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Tax>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Taxes).Returns(mockDbSet.Object);

            var repo = new TaxRepository(context.Object);
            repo.Change(new Tax() { Id = 3, Name = "Medicine", Percent = 3 });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }
    }
}
