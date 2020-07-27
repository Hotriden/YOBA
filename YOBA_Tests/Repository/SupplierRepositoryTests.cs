using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.UOF.Repository;
using FluentAssertions;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.DAL;
using System.Threading.Tasks;

namespace ProductServiceTest
{
    [TestFixture]
    public class SupplierRepositoryTests
    {
        [Test]
        public async Task SupplierRepo_Add()
        {
            var mockDbSet = new Mock<DbSet<Supplier>>();
            var mockContext = new Mock<YOBAContext>();

            mockContext.Setup(c => c.Supplier).Returns(mockDbSet.Object);
            var res = new SupplierRepository(mockContext.Object);

            await res.Add("Vasyan11", new Supplier() { Address="Washington st 12", Id= 1, SupplierName="Bington LTD" });

            mockContext.Verify(s => s.Add(It.IsAny<Supplier>()), Times.Once());
            mockContext.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public void SupplierRepo_GetByID()
        {
            var data = new List<Supplier>() {
                new Supplier() { Address="Washington st 12", Id=1, SupplierName="Bington LTD"},
                new Supplier() { Address="Clinton ave 12", Id=2, SupplierName="WashStore LTD"},
                new Supplier() { Address="Gabboni st 144", Id=11, SupplierName="DCH LTD"},
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Supplier>>();
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Supplier).Returns(mockDbSet.Object);

            var repo = new SupplierRepository(context.Object);
            var result = repo.Get("Vasyan11", new Supplier() { SupplierName = "DCH LTD" });

            Assert.IsTrue(result.SupplierName == "DCH LTD");
        }

        [Test]
        public void SupplierRepo_GetAll()
        {
            var data = new List<Supplier>() {
                new Supplier() { Address="Washington st 12", Id=1, SupplierName="Bington LTD", UserId="gfdg34"},
                new Supplier() { Address="Clinton ave 12", Id=2, SupplierName="WashStore LTD", UserId="gfdg34"},
                new Supplier() { Address="Gabboni st 144", Id=1, SupplierName="DCH LTD", UserId="gfdg34"},
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Supplier>>();
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Supplier).Returns(mockDbSet.Object);

            var repo = new SupplierRepository(context.Object);
            var result = repo.GetAll("gfdg34").ToList();

            result.Should().AllBeOfType(typeof(Supplier));
            result.Should().HaveCount(3);
            result.Should().Contain(x => x.SupplierName == "WashStore LTD");
        }

        [Test]
        public async Task SupplierRepo_Delete()
        {
            var data = new List<Supplier>() {
                new Supplier() { Address="Washington st 12", Id=1, SupplierName="Bington LTD"},
                new Supplier() { Address="Clinton ave 12", Id=2, SupplierName="WashStore LTD"},
                new Supplier() { Address="Gabboni st 144", Id=11, SupplierName="DCH LTD"},
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Supplier>>();
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Supplier).Returns(mockDbSet.Object);

            var repo = new SupplierRepository(context.Object);
            await repo.Delete("Vasyan11", new Supplier()
            {
                Id= 11,
                Address = "Gabboni st 144",
                SupplierName = "DCH LTD"
            });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public async Task SupplierRepo_Update()
        {
            var data = new List<Supplier>() {
                new Supplier() { Address="Washington st 12", Id=1, SupplierName="Bington LTD"},
                new Supplier() { Address="Clinton ave 12", Id=2, SupplierName="WashStore LTD"},
                new Supplier() { Address="Gabboni st 144", Id=11, SupplierName="DCH LTD"},
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Supplier>>();
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Supplier).Returns(mockDbSet.Object);

            var repo = new SupplierRepository(context.Object);
            await repo.Change("Vasyan11", new Supplier() { Address = "Gabboni st 144", Id = 11, SupplierName = "DCH LTD" });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }
    }
}
