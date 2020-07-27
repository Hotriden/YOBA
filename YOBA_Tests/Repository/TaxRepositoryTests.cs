using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.UOF.Repository;
using FluentAssertions;
using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_LibraryData.DAL;
using System.Threading.Tasks;

namespace ProductServiceTest
{
    public class TaxRepositoryTests
    {
        [Test]
        public async Task TaxRepo_Add()
        {
            var mockDbSet = new Mock<DbSet<Tax>>();
            var mockContext = new Mock<YOBAContext>();

            mockContext.Setup(c => c.Tax).Returns(mockDbSet.Object);
            var res = new TaxRepository(mockContext.Object);

            await res.Add("Vasyan11", new Tax() { Id = 1, Name = "Freight", Percent = 20 });

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
            context.Setup(s => s.Tax).Returns(mockDbSet.Object);

            var repo = new TaxRepository(context.Object);
            var result = repo.Get("Vasyan11", new Tax() { Name = "VAT" });

            Assert.IsTrue(result.Name == "VAT");
        }

        [Test]
        public void TaxRepo_GetAll()
        {
            var data = new List<Tax>() {
                new Tax() { Id=1, Name = "Freight", Percent = 20, UserId="bibka228"},
                new Tax() { Id=101, Name = "VAT", Percent = 18, UserId="bibka228"},
                new Tax(){Id=3, Name="Medicine", Percent=3, UserId="bibka228"}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Tax>>();
            mockDbSet.As<IQueryable<Tax>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Tax>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Tax>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Tax>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Tax).Returns(mockDbSet.Object);

            var repo = new TaxRepository(context.Object);
            var result = repo.GetAll("bibka228").ToList();

            result.Should().AllBeOfType(typeof(Tax));
            result.Should().HaveCount(3);
            result.Should().Contain(x => x.Name == "VAT");
        }

        [Test]
        public async Task TaxRepo_Delete()
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
            context.Setup(s => s.Tax).Returns(mockDbSet.Object);

            var repo = new TaxRepository(context.Object);
            await repo.Delete("Vasyan11", new Tax() {
                Id = 3,
                Name = "Medicine",
                Percent = 3 });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public async Task TaxRepo_Update()
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
            context.Setup(s => s.Tax).Returns(mockDbSet.Object);

            var repo = new TaxRepository(context.Object);
            await repo.Change("Vasyan11", new Tax() { Id = 3, Name = "Medicine", Percent = 3 });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }
    }
}
