using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL;
using YOBA_LibraryData.BLL.UOF.Repository;
using FluentAssertions;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.DAL;
using System.Threading.Tasks;
using YOBA_LibraryData.BLL.Interfaces;
using YOBA_LibraryData.BLL.UOF.Interfaces;

namespace ProductServiceTest
{
    public class WareHouseRepositoryTests
    {
        [Test]
        public async Task WareHouseRepo_Add()
        {
            var mockDbSet = new Mock<DbSet<WareHouse>>();
            var mockContext = new Mock<YOBAContext>();

            mockContext.Setup(c => c.WareHouse).Returns(mockDbSet.Object);
            var res = new WareHouseRepository(mockContext.Object);

            await res.Add("asdqqwe15", new WareHouse() { Id = 1, Address = "Lincoln St. 12", WareHouseName = "WareHouse#1" });
            mockContext.Verify(s => s.Add(It.IsAny<WareHouse>()), Times.Once());
            mockContext.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public void WareHouseRepo_GetByID()
        {
            var data = new List<WareHouse>() {
                new WareHouse() { Id=1, Address="Lincoln St. 12", WareHouseName="WareHouse#1", UserId="asdaq111"},
                new WareHouse() { Id=101, Address="Washington St. 25", WareHouseName="WareHouse#101", UserId="asdaq111"}
            }.AsQueryable();

            var repo = new Mock<IWareHouseRepository>();
            repo.As<IQueryable<WareHouse>>().Setup(x => x.Provider).Returns(data.Provider);
            repo.As<IQueryable<WareHouse>>().Setup(x => x.Expression).Returns(data.Expression);
            repo.As<IQueryable<WareHouse>>().Setup(x => x.ElementType).Returns(data.ElementType);
            repo.As<IQueryable<WareHouse>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            //var result = repo.Object.GetById("asdaq111", 101);

            //Assert.IsTrue(result.WareHouseName == "WareHouse#101");
        }

        [Test]
        public void WareHouseRepo_GetAll()
        {
            var data = new List<WareHouse>() {
                new WareHouse() { Id=1, Address="Lincoln St. 12", WareHouseName="WareHouse#1", UserId="asdqqwe15" },
                new WareHouse() { Id=101, Address="Washington St. 25", WareHouseName="WareHouse#101", UserId="asdqqwe15" },
                new WareHouse() { Id=201, Address="Morrison ave. 2", WareHouseName="WareHouse#201", UserId="asdqqwe15" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<WareHouse>>();
            mockDbSet.As<IQueryable<WareHouse>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<WareHouse>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<WareHouse>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<WareHouse>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.WareHouse).Returns(mockDbSet.Object);

            var repo = new WareHouseRepository(context.Object);
            var result = repo.GetAll("asdqqwe15").ToList();

            result.Should().AllBeOfType(typeof(WareHouse));
            result.Should().HaveCount(3);
            result.Should().Contain(x => x.WareHouseName == "WareHouse#201");
        }

        [Test]
        public async Task WareHouseRepo_Delete()
        {
            var data = new List<WareHouse>() {
                new WareHouse() { Id=1, Address="Lincoln St. 12", WareHouseName="WareHouse#1", UserId="someid1211"},
                new WareHouse() { Id=101, Address="Washington St. 25", WareHouseName="WareHouse#101", UserId="someid1211" },
                new WareHouse() { Id=201, Address="Morrison ave. 2", WareHouseName="WareHouse#201", UserId="someid1211" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<WareHouse>>();
            mockDbSet.As<IQueryable<WareHouse>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<WareHouse>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<WareHouse>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<WareHouse>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.WareHouse).Returns(mockDbSet.Object);

            var repo = new WareHouseRepository(context.Object);
            await repo.Delete("someid1211", new WareHouse()
            {
                Id = 201,
                Address = "Morrison ave. 2",
                WareHouseName = "WareHouse#201"
            });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public async Task WareHouseRepo_Update()
        {
            var data = new List<WareHouse>() {
                new WareHouse() { Id=1, Address="Lincoln St. 12", WareHouseName="WareHouse#1"},
                new WareHouse() { Id=101, Address="Washington St. 25", WareHouseName="WareHouse#101" },
                new WareHouse() { Id=201, Address="Morrison ave. 2", WareHouseName="WareHouse#201" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<WareHouse>>();
            mockDbSet.As<IQueryable<WareHouse>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<WareHouse>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<WareHouse>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<WareHouse>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.WareHouse).Returns(mockDbSet.Object);

            var repo = new WareHouseRepository(context.Object);
            await repo.Change("someid1211", new WareHouse() { Id = 201, Address = "Morrison ave. 2", WareHouseName = "WareHouse#201" });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }
    }
}
