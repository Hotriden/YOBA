using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL;
using YOBA_LibraryData.BLL.UOF.Repository;
using FluentAssertions;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_LibraryData.DAL;

namespace ProductServiceTest
{
    class ExpenceRepositoryTests
    {
        [Test]
        public void ExpenceRepo_Add()
        {
            var mockDbSet = new Mock<DbSet<Expence>>();
            var mockContext = new Mock<YOBAContext>();

            mockContext.Setup(c => c.Expences).Returns(mockDbSet.Object);
            var res = new ExpenceRepository(mockContext.Object);

            res.Add(new Expence() { Id= "1", Name="Transport", Value=200 });
            res.Add(new Expence() { Id= "2", Name="Rent", Value=50 });

            mockContext.Verify(s => s.Add(It.IsAny<Expence>()), Times.Exactly(2));
            mockContext.Verify(s => s.SaveChanges(), Times.Exactly(2));
        }

        [Test]
        public void ExpenceRepo_GetByID()
        {
            var data = new List<Expence>() {
                new Expence() { Id="1", Name="Transport", Value=200 },
                new Expence() { Id="2", Name="Rent", Value=50 },
                new Expence() { Id="5", Name="Market Promotion", Value=150 }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Expence>>();
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Expences).Returns(mockDbSet.Object);

            var repo = new ExpenceRepository(context.Object);
            var result = repo.GetById("2");

            Assert.IsTrue(result.Name == "Rent");
        }

        [Test]
        public void ExpenceRepo_GetAll()
        {
            var data = new List<Expence>() {
                new Expence() {Id="1", Name="Transport", Value=200 },
                new Expence() {Id="2", Name="Rent", Value=50},
                new Expence() {Id="5", Name="Market Promotion", Value=150},
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Expence>>();
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Expences).Returns(mockDbSet.Object);

            var repo = new ExpenceRepository(context.Object);
            var result = repo.GetAll().ToList();

            result.Should().AllBeOfType(typeof(Expence));
            result.Should().HaveCount(3);
            result.Should().Contain(x => x.Name == "Transport");
        }

        [Test]
        public void ExpenceRepo_Delete()
        {
            var data = new List<Expence>() {
                new Expence() {Id="1", Name="Transport", Value=200 },
                new Expence() {Id="2", Name="Rent", Value=50},
                new Expence() {Id="5", Name="Market Promotion", Value=150},
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Expence>>();
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Expences).Returns(mockDbSet.Object);

            var repo = new ExpenceRepository(context.Object);

            repo.Delete(new Expence()
            {
                Id = "5",
                Name = "Market Promotion",
                Value = 150
            });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public void ExpenceRepo_Update()
        {
            var data = new List<Expence>() {
                new Expence() {Id="1", Name="Transport", Value=200 },
                new Expence() {Id="2", Name="Rent", Value=50},
                new Expence() {Id="5", Name="Market Promotion", Value=150},
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Expence>>();
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Expence>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Expences).Returns(mockDbSet.Object);

            var repo = new ExpenceRepository(context.Object);
            repo.Change(new Expence() { Id = "5", Name = "Market Promotion", Value = 300 });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }
    }
}
