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
    class IncomeRepositoryTests
    {
        [Test]
        public void IncomeRepo_Add()
        {
            var mockDbSet = new Mock<DbSet<Income>>();
            var mockContext = new Mock<YOBAContext>();

            mockContext.Setup(c => c.Incomes).Returns(mockDbSet.Object);
            var res = new IncomeRepository(mockContext.Object);

            res.Add(new Income() { Id= 1, Name="Net income", Value=3000});

            mockContext.Verify(s => s.Add(It.IsAny<Income>()), Times.Once());
            mockContext.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public void IncomeRepo_GetByID()
        {
            var data = new List<Income>() {
                new Income() { Id=1, Name="Net income", Value=3000 },
                new Income() { Id=2, Name="Another income", Value=200}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Income>>();
            mockDbSet.As<IQueryable<Income>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Income>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Income>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Income>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Incomes).Returns(mockDbSet.Object);

            var repo = new IncomeRepository(context.Object);
            var result = repo.GetById(1);

            Assert.IsTrue(result.Name == "Net income");
        }

        [Test]
        public void IncomeRepo_GetAll()
        {
            var data = new List<Income>() {
                new Income() { Id=1, Name="Net income", Value=3000 },
                new Income() { Id=2, Name="Another income", Value=200}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Income>>();
            mockDbSet.As<IQueryable<Income>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Income>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Income>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Income>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Incomes).Returns(mockDbSet.Object);

            var repo = new IncomeRepository(context.Object);
            var result = repo.GetAll().ToList();

            result.Should().AllBeOfType(typeof(Income));
            result.Should().HaveCount(2);
            result.Should().Contain(x => x.Name == "Another income");
        }

        [Test]
        public void IncomeRepo_Delete()
        {
            var data = new List<Income>() {
                new Income() { Id=1, Name="Net income", Value=3000 },
                new Income() { Id=2, Name="Another income", Value=200}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Income>>();
            mockDbSet.As<IQueryable<Income>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Income>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Income>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Income>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Incomes).Returns(mockDbSet.Object);

            var repo = new IncomeRepository(context.Object);

            repo.Delete(new Income()
            {
                Id = 1,
                Name = "Net income",
                Value = 3000
            });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public void IncomeRepo_Update()
        {
            var data = new List<Income>() {
                new Income() { Id=1, Name="Net income", Value=3000 },
                new Income() { Id=2, Name="Another income", Value=200}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Income>>();
            mockDbSet.As<IQueryable<Income>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Income>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Income>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Income>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Incomes).Returns(mockDbSet.Object);

            var repo = new IncomeRepository(context.Object);
            repo.Change(new Income() { Id= 2, Name = "Sales income" });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }
    }
}
