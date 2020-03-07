using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL;
using YOBA_LibraryData.BLL.UOF.Repository;
using FluentAssertions;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.BLL.Entities.Sell;
using YOBA_LibraryData.DAL;
using System.Threading.Tasks;

namespace ProductServiceTest
{
    public class BranchRepositoryTests
    {
        [Test]
        public async Task BranchRepo_Add()
        {
            var mockDbSet = new Mock<DbSet<Branch>>();
            var mockContext = new Mock<YOBAContext>();

            mockContext.Setup(c => c.Branches).Returns(mockDbSet.Object);
            var res = new BranchRepository(mockContext.Object);

            await res.Add(new Branch() { BranchId = 1, BranchName = "Finance" });

            mockContext.Verify(s => s.Add(It.IsAny<Branch>()), Times.Once());
            mockContext.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public void BranchRepo_GetByID()
        {
            var data = new List<Branch>() {
                new Branch() { BranchId=1, BranchName="Finance"},
                new Branch() { BranchId=2, BranchName="Sells" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Branch>>();
            mockDbSet.As<IQueryable<Branch>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Branch>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Branch>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Branch>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Branches).Returns(mockDbSet.Object);

            var repo = new BranchRepository(context.Object);
            var result = repo.GetById(2);

            Assert.IsTrue(result.BranchName == "Sells");
        }

        [Test]
        public void BranchRepo_GetAll()
        {
            var data = new List<Branch>() {
                new Branch() { BranchId=1, BranchName="Finance"},
                new Branch() { BranchId=2, BranchName="Sells" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Branch>>();
            mockDbSet.As<IQueryable<Branch>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Branch>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Branch>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Branch>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(c => c.Branches).Returns(mockDbSet.Object);

            var repo = new BranchRepository(context.Object);
            var result = repo.GetAll().ToList();

            result.Should().AllBeOfType(typeof(Branch));
            result.Should().HaveCount(2);
            result.Should().Contain(x => x.BranchName == "Finance");
        }

        [Test]
        public async Task BranchRepo_Delete()
        {
            var data = new List<Branch>() {
                new Branch() { BranchId=1, BranchName="Finance"},
                new Branch() { BranchId=2, BranchName="Sells" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Branch>>();
            mockDbSet.As<IQueryable<Branch>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Branch>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Branch>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Branch>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(c => c.Branches).Returns(mockDbSet.Object);
            var repo = new BranchRepository(context.Object);
            await repo.Delete(new Branch() { BranchId = 2, BranchName = "Sells" });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public async Task BranchRepo_Update()
        {
            var data = new List<Branch>() {
                new Branch() { BranchId=1, BranchName="Finance"},
                new Branch() { BranchId=2, BranchName="Sells" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Branch>>();
            mockDbSet.As<IQueryable<Branch>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Branch>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Branch>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Branch>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(c => c.Branches).Returns(mockDbSet.Object);
            var repo = new BranchRepository(context.Object);
            await repo.Change(new Branch() { BranchId = 2, BranchName = "Sells" });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }
    }
}
