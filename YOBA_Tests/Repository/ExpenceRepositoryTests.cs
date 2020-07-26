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
    class ExpenceRepositoryTests
    {
        [Test]
        public async Task ExpenceRepo_Add()
        {
            var mockDbSet = new Mock<DbSet<Expence>>();
            var mockContext = new Mock<YOBAContext>();

            mockContext.Setup(c => c.Expences).Returns(mockDbSet.Object);
            var res = new ExpenceRepository(mockContext.Object);

            await res.Add("Vasyan11", new Expence() { Id= 1, Name="Transport", Value=200 });
            await res.Add("Vasyan11", new Expence() { Id= 2, Name="Rent", Value=50 });

            mockContext.Verify(s => s.Add(It.IsAny<Expence>()), Times.Exactly(2));
            mockContext.Verify(s => s.SaveChanges(), Times.Exactly(2));
        }

        [Test]
        public void ExpenceRepo_GetByID()
        {
            var data = new List<Expence>() {
                new Expence() { Id=1, Name="Transport", Value=200 },
                new Expence() { Id=2, Name="Rent", Value=50 },
                new Expence() { Id=5, Name="Market Promotion", Value=150 }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Expence>>();
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Expences).Returns(mockDbSet.Object);

            var repo = new ExpenceRepository(context.Object);
            var result = repo.GetById("Vasyan11", 2);

            Assert.IsTrue(result.Name == "Rent");
        }

        [Test]
        public void ExpenceRepo_GetAll()
        {
            var data = new List<Expence>() {
                new Expence() {Id=1, Name="Transport", Value=200, UserId="vasya33" },
                new Expence() {Id=2, Name="Rent", Value=50, UserId="vasya33"},
                new Expence() {Id=5, Name="Market Promotion", Value=150, UserId="vasya33"},
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Expence>>();
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Expences).Returns(mockDbSet.Object);

            var repo = new ExpenceRepository(context.Object);
            var result = repo.GetAll("vasya33").ToList();

            result.Should().AllBeOfType(typeof(Expence));
            result.Should().HaveCount(3);
            result.Should().Contain(x => x.Name == "Transport");
        }

        [Test]
        public async Task ExpenceRepo_Delete()
        {
            var data = new List<Expence>() {
                new Expence() {Id=1, Name="Transport", Value=200 },
                new Expence() {Id=2, Name="Rent", Value=50},
                new Expence() {Id=5, Name="Market Promotion", Value=150},
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Expence>>();
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Expences).Returns(mockDbSet.Object);

            var repo = new ExpenceRepository(context.Object);

            await repo.Delete("Vasyan11", new Expence()
            {
                Id = 5,
                Name = "Market Promotion",
                Value = 150
            });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public async Task ExpenceRepo_Update()
        {
            var data = new List<Expence>() {
                new Expence() {Id=1, Name="Transport", Value=200 },
                new Expence() {Id=2, Name="Rent", Value=50},
                new Expence() {Id=5, Name="Market Promotion", Value=150},
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Expence>>();
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Expences).Returns(mockDbSet.Object);

            var repo = new ExpenceRepository(context.Object);
            await repo.Change("Vasyan11", new Expence() { Id = 5, Name = "Market Promotion", Value = 300 });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }
    }
}
